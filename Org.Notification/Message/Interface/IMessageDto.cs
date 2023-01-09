namespace Org.Notification.Message.Interface
{
    /// <summary>
    /// Base for any message in the system
    /// </summary>
    public interface IMessageDto
    {
        public Guid Id { set; get; }

        public DateTime? At { set; get; }
    }
}
