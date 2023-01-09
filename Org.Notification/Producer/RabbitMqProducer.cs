using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Org.Notification.Configuration;
using Org.Notification.Producer.Interface;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Org.Notification.Producer
{
    internal class RabbitMqProducer : IMessageProducer
    {
        private readonly RabbitMqSettings _rabbitMqSettings;
        private readonly IConnection? _connection;
        private IModel? _channel;

        public RabbitMqProducer(IOptions<RabbitMqSettings> options)
        {
            _rabbitMqSettings = options.Value;
            var connectionFactory = new ConnectionFactory { HostName = _rabbitMqSettings.HostName ?? "localhost" };
            _connection = connectionFactory.CreateConnection();
        }

        /// <inheritdoc cref="IMessageProducer"/>
        public string? CreateMessageCollectionIfNotExist(string collectionName)
        {
            _channel = _connection?.CreateModel();
            var queueDeclareOk = _channel?.QueueDeclare(collectionName,
                durable: false,
                exclusive: false,
                autoDelete: _rabbitMqSettings.AutoDeleteQueue ?? false,
                arguments: null);

            return queueDeclareOk?.QueueName;
        }

        /// <inheritdoc cref="IMessageProducer"/>
        public async Task SendMessageAsync(string collectionName, object message, CancellationToken cancellationToken)
        {
            CreateMessageCollectionIfNotExist(collectionName);

            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);
            await Task.Run(() => _channel.BasicPublish(exchange: "", routingKey: collectionName, basicProperties: null, body: body), cancellationToken);
        }

        /// <inheritdoc cref="IMessageProducer"/>
        public void DoSubscription(string collectionName, Action<object?> callback)
        {
            CreateMessageCollectionIfNotExist(collectionName);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (_, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                callback.Invoke(JsonSerializer.Deserialize(message, typeof(object)));
            };

            _channel.BasicConsume(queue: collectionName,
                autoAck: _rabbitMqSettings.AutoAck ?? true,
                consumer: consumer);
        }

        /// <inheritdoc cref="IMessageProducer"/>
        public void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
        }
    }
}
