using Org.Notification.Core;

namespace Org.Notification.Publisher.Interface
{
    public interface IPublisher
    {
        Task<IList<Guid>> NotifyAsync(NotificationMessage message);
    }
}
