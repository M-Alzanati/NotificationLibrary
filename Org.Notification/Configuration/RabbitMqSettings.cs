namespace Org.Notification.Configuration
{
    public record RabbitMqSettings
    {
        public string HostName { get; set; }

        public bool AutoDeleteQueue { set; get; }

        public bool AutoAck { set; get; }
    }
}
