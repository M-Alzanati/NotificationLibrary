namespace Org.Notification.Producer.Interface
{
    public interface IMessageProducer : IDisposable
    {
        string? CreateMessageProducerIfNotExist(string queueName);

        Task SendMessageAsync(string queueName, object message);

        void DoSubscription(string queueName, Action<object?> action);
    }
}
