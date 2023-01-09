using Org.Notification.Message.Interface;

namespace Org.Notification.Core
{
    /// <summary>
    ///  Notification message which contains list of detailed messages so if we have multiple publisher we can handle them separately
    /// </summary>
    public class NotificationMessage
    {
        /// <summary>
        /// Message Id
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// List of detailed messages for publishers
        /// </summary>
        public IList<IMessageDto?> DetailMessage { get; }

        /// <summary>
        /// We don't have to manage message id by our self if it's required, otherwise we will generate new guid
        /// </summary>
        /// <param name="id"></param>
        public NotificationMessage(Guid? id = default)
        {
            if (id == null)
            {
                Id = Guid.NewGuid();
            }

            DetailMessage = new List<IMessageDto?>();
        }
    }
}
