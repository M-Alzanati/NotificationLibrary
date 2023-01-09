using Org.Notification.Core;

namespace Org.Notification.Publisher.Interface
{
    /// <summary>
    /// Implementation for observer for the publisher
    /// </summary>
    public interface IPublisher
    {
        /// <summary>
        /// Send message to this publisher async
        /// </summary>
        /// <param name="message"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<Guid>> NotifyAsync(NotificationMessage message, CancellationToken cancellationToken);

        /// <summary>
        /// Get the publisher name
        /// </summary>
        /// <returns></returns>
        string GetPublisherName();
    }
}
