using Org.Notification.Message.Interface;

namespace Org.Notification.Message
{
    /// <summary>
    /// Sms Message Dto which can be assigned to a publisher
    /// </summary>
    public class SmsMessageDto : IMessageDto
    {
        public string? RecipientPhone { set; get; }

        public string? Body { set; get; }

        public Guid Id { get; set; }

        public DateTime? At { get; set; }
    }
}
