namespace Org.Notification.Producer
{
    public sealed class RabbitMqSettings
    {
        public string HostName { get; set; }

        public bool AutoDeleteQueue { set; get; }

        public bool AutoAck { set; get; }
    }
}
