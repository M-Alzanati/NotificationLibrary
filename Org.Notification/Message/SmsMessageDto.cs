using System.Text.Json;

namespace Org.Notification.Message
{
    public class SmsMessageDto : IMessageDto
    {
        public string? RecipientPhone { set; get; }

        public string? Body { set; get; }

        public Guid Id { get; set; }
    }
}
