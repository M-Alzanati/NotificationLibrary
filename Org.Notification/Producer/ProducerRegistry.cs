using System.Collections.Concurrent;
using Org.Notification.Producer.Interface;
using Org.Notification.Publisher.Interface;
using Org.Notification.Subscription.Base;

namespace Org.Notification.Producer
{
    internal class ProducerRegistry : IProducerRegistry
    {
        private static readonly ConcurrentDictionary<string, IPublisher> Publishers;
        private static readonly ConcurrentDictionary<string, ICommand> Workers;

        static ProducerRegistry()
        {
            Publishers = new ConcurrentDictionary<string, IPublisher>();
            Workers = new ConcurrentDictionary<string, ICommand>();
        }

        /// <inheritdoc cref="IProducerRegistry"/>
        public string GetPublisherName<TService>() where TService : IPublisher
        {
            return Publishers.SingleOrDefault(e => e.Value is TService).Key;
        }

        /// <inheritdoc cref="IProducerRegistry"/>
        public string GetWorkerName<TService>() where TService : ICommand
        {
            return Workers.SingleOrDefault(e => e.Value is TService).Key;
        }

        /// <inheritdoc cref="IProducerRegistry"/>
        public IPublisher GetPublisher<TService>() where TService : IPublisher
        {
            return Publishers.SingleOrDefault(e => e.Value is TService).Value;
        }

        /// <inheritdoc cref="IProducerRegistry"/>
        public ICommand GetWorker<TService>() where TService : ICommand
        {
            return Workers.SingleOrDefault(e => e.Value is TService).Value;
        }

        /// <inheritdoc cref="IProducerRegistry"/>
        public IEnumerable<KeyValuePair<string, ICommand>> GetWorkers()
        {
            return Workers;
        }

        /// <inheritdoc cref="IProducerRegistry"/>
        public void RegisterProducer(IPublisher publisher, ICommand worker, string name, bool overrideDefault = false)
        {
            var uniqueId = Guid.NewGuid();
            Publishers.TryAdd(overrideDefault ? $"{name}_{uniqueId}" : $"{name}", publisher);
            Workers.TryAdd(overrideDefault ? $"{name}_{uniqueId}" : $"{name}", worker);
        }

        /// <inheritdoc cref="IDisposable"/>
        public void Dispose()
        {
            Publishers.Clear();
            Workers.Clear();
        }
    }
}
