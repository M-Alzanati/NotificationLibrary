using Org.Notification.Core;

namespace Org.Notification.Publisher.Interface
{
    public interface IPublisher
    {
        Task<IEnumerable<Guid>> NotifyAsync(NotificationMessage message, CancellationToken cancellationToken);
    }
}
