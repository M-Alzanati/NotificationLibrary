using Org.Notification.Core;

namespace Org.Notification.Publisher.Interface
{
    public interface INotificationPublisher
    {
        INotificationPublisher Subscribe<TService>() where TService : IPublisher;

        INotificationPublisher UnSubscribe<TService>() where TService : IPublisher;

        INotificationPublisher UnSubscribeAll();

        Task<IEnumerable<Guid>> NotifyAsync(NotificationMessage notificationMessage);
    }
}
