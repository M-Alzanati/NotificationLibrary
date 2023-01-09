using Org.Notification.Core;

namespace Org.Notification.Publisher.Interface
{
    public interface INotificationPublisher : IPublisher
    {
        INotificationPublisher Subscribe<TService>() where TService : IPublisher;

        INotificationPublisher UnSubscribe<TService>() where TService : IPublisher;

        INotificationPublisher UnSubscribeAll();
    }
}
