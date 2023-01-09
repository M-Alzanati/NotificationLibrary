namespace Org.Notification.Configuration
{
    /// <summary>
    /// RabbitMQ settings
    /// </summary>
    public record RabbitMqSettings
    {
        public string? HostName { get; set; }

        public bool? AutoDeleteQueue { set; get; }

        public bool? AutoAck { set; get; }
    }
}
