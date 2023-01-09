namespace Org.Notification.Producer.Interface
{
    public interface IMessageProducer : IDisposable
    {
        string? CreateMessageProducerIfNotExist(string collectionName);

        Task SendMessageAsync(string collectionName, object message, CancellationToken cancellationToken);

        void DoSubscription(string collectionName, Action<object?> action);
    }
}
