using Org.Notification.Message.Interface;

namespace Org.Notification.Message
{
    /// <summary>
    /// Email Message Dto which can be assigned to a publisher
    /// </summary>
    public class EmailMessageDto : IMessageDto
    {
        public string? RecipientEmail { get; set; }

        public string? Body { set; get; }

        public Guid Id { get; set; }

        public DateTime? At { get; set; }
    }
}
