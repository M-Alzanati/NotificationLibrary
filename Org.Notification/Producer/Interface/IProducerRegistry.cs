using Org.Notification.Publisher.Interface;
using Org.Notification.Subscription.Base;

namespace Org.Notification.Producer.Interface
{
    /// <summary>
    /// This is in-memory implementation to know what which publisher assigned to which consumer
    /// </summary>
    public interface IProducerRegistry : IDisposable
    {
        /// <summary>
        /// Get publisher name for service if it's implement IPublisher
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        string GetPublisherName<TService>() where TService : IPublisher;

        /// <summary>
        /// Get publisher name for service if it's implement ICommand
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        string GetWorkerName<TService>() where TService : ICommand;

        /// <summary>
        /// Get publisher instance for service if it's implement IPublisher
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        IPublisher GetPublisher<TService>() where TService : IPublisher;

        /// <summary>
        /// Get worker instance for service if it's implement ICommand
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        ICommand GetWorker<TService>() where TService : ICommand;

        /// <summary>
        /// Get all workers
        /// </summary>
        /// <returns></returns>
        IEnumerable<KeyValuePair<string, ICommand>> GetWorkers();

        /// <summary>
        /// Assign a publisher to specific worker in that collection
        /// This collection can be queue or table in database, or in-memory implementation.
        /// </summary>
        /// <param name="publisher"></param>
        /// <param name="worker"></param>
        /// <param name="collectionName"></param>
        /// <param name="overrideDefault"></param>
        void RegisterProducer(IPublisher publisher, ICommand worker, string collectionName, bool overrideDefault = false);
    }
}
