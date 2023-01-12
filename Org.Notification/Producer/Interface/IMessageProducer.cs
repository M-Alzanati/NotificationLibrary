namespace Org.Notification.Producer.Interface
{
    /// <summary>
    /// Using Message producer we can send messages and subscribe to messages using any external or internal implementation.
    /// Currently this library support RabbitMQ
    /// </summary>
    public interface IMessageProducer : IDisposable
    {
        /// <summary>
        /// Create message collection, this collection can be queue or table in database, or in-memory implementation.
        /// </summary>
        /// <param name="collectionName"></param>
        /// <returns></returns>
        string? CreateMessageCollectionIfNotExist(string collectionName);

        /// <summary>
        /// Send message to a specific collection, this collection can be queue or table in database, or in-memory implementation.
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="message"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task SendMessageAsync(string collectionName, object message, CancellationToken cancellationToken);

        /// <summary>
        /// Subscribe to a collection and execute this action when something happens to this collection
        /// This collection can be queue or table in database, or in-memory implementation.
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="callback"></param>
        void DoSubscription(string collectionName, Func<object?, Task> callback);
    }
}
