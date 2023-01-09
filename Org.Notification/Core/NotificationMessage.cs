namespace Org.Notification.Core
{
    public class NotificationMessage
    {
        public Guid Id { get; }

        public IList<IMessageDto?> DetailMessage { get; }

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
