namespace Org.Notification.Message
{
    public class EmailMessageDto : IMessageDto
    {
        public string? RecipientEmail { get; set; }

        public string? Body { set; get; }

        public Guid Id { get; set; }
    }
}
