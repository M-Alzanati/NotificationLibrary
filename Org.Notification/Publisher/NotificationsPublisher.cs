using Org.Notification.Core;
using Org.Notification.Producer.Interface;
using Org.Notification.Publisher.Interface;

namespace Org.Notification.Publisher
{
    internal class NotificationsPublisher : INotificationsPublisher
    {
        private readonly List<IPublisher> _publishers;
        private readonly IProducerRegistry _producerRegistry;

        public NotificationsPublisher(IProducerRegistry producerRegistry)
        {
            _publishers = new List<IPublisher>();
            _producerRegistry = producerRegistry;
        }

        /// <summary>
        /// <inheritdoc cref="INotificationsPublisher"/>
        /// </summary>
        public INotificationsPublisher Subscribe<TService>() where TService : IPublisher
        {
            var publisher = _producerRegistry.GetPublisher<TService>();
            
            if (!_publishers.Contains(publisher))
            {
                _publishers.Add(publisher);
            }

            return this;
        }

        /// <summary>
        /// <inheritdoc cref="INotificationsPublisher"/>
        /// </summary>
        public INotificationsPublisher UnSubscribe<TService>() where TService : IPublisher
        {
            var publisher = _producerRegistry.GetPublisher<TService>();

            if (!_publishers.Contains(publisher))
            {
                _publishers.Remove(publisher);
            }

            return this;
        }

        /// <summary>
        /// <inheritdoc cref="INotificationsPublisher"/>
        /// </summary>
        public INotificationsPublisher UnSubscribeAll()
        {
            _publishers.Clear();
            return this;
        }

        /// <summary>
        /// <inheritdoc cref="INotificationsPublisher"/>
        /// </summary>
        public async Task<IEnumerable<Guid>> NotifyAsync(NotificationMessage notificationMessage, CancellationToken cancellationToken)
        {
            var list = new List<Guid>();

            foreach (var publisher in _publishers)
            {
                list.AddRange(await publisher.NotifyAsync(notificationMessage, cancellationToken));
            }

            return list.Distinct();
        }

        /// <summary>
        /// <inheritdoc cref="IPublisher"/>
        /// </summary>
        public string GetPublisherName()
        {
            return nameof(NotificationsPublisher);
        }
    }
}
