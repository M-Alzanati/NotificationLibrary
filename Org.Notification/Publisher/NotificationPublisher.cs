using Org.Notification.Core;
using Org.Notification.Producer.Interface;
using Org.Notification.Publisher.Interface;

namespace Org.Notification.Publisher
{
    internal class NotificationPublisher : INotificationPublisher
    {
        private readonly List<IPublisher> _publishers;
        private readonly IProducerRegistry _producerRegistry;

        public NotificationPublisher(IProducerRegistry producerRegistry)
        {
            _publishers = new List<IPublisher>();
            _producerRegistry = producerRegistry;
        }

        public INotificationPublisher Subscribe<TService>() where TService : IPublisher
        {
            var publisher = _producerRegistry.GetPublisher<TService>();
            
            if (!_publishers.Contains(publisher))
            {
                _publishers.Add(publisher);
            }

            return this;
        }

        public INotificationPublisher UnSubscribe<TService>() where TService : IPublisher
        {
            var publisher = _producerRegistry.GetPublisher<TService>();

            if (!_publishers.Contains(publisher))
            {
                _publishers.Remove(publisher);
            }

            return this;
        }

        public INotificationPublisher UnSubscribeAll()
        {
            _publishers.Clear();
            return this;
        }

        public async Task<IEnumerable<Guid>> NotifyAsync(NotificationMessage notificationMessage)
        {
            var list = new List<Guid>();

            foreach (var publisher in _publishers)
            {
                list.AddRange(await publisher.NotifyAsync(notificationMessage));
            }

            return list.Distinct();
        }
    }
}
