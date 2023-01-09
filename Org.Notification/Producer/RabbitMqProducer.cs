using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Org.Notification.Producer.Interface;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Org.Notification.Producer
{
    internal class RabbitMqProducer : IRabbitMqProducer
    {
        private readonly RabbitMqSettings _rabbitMqSettings;
        private readonly IConnection? _connection;
        private IModel? _channel;

        public RabbitMqProducer(IOptions<RabbitMqSettings> options)
        {
            _rabbitMqSettings = options.Value;
            var connectionFactory = new ConnectionFactory { HostName = _rabbitMqSettings.HostName };
            _connection = connectionFactory.CreateConnection();
        }

        public string? CreateMessageProducerIfNotExist(string queueName)
        {
            _channel = _connection?.CreateModel();
            var queueDeclareOk = _channel?.QueueDeclare(queueName,
                durable: false,
                exclusive: false,
                autoDelete: _rabbitMqSettings.AutoDeleteQueue,
                arguments: null);

            return queueDeclareOk?.QueueName;
        }

        public async Task SendMessageAsync(string queueName, object message)
        {
            CreateMessageProducerIfNotExist(queueName);

            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);
            await Task.Run(() => _channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body));
        }

        public void DoSubscription(string queueName, Action<object?> action)
        {
            CreateMessageProducerIfNotExist(queueName);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                action.Invoke(JsonSerializer.Deserialize(message, typeof(object)));
            };

            _channel.BasicConsume(queue: queueName,
                autoAck: _rabbitMqSettings.AutoAck,
                consumer: consumer);
        }

        public void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
        }
    }
}
