namespace Org.Notification.Configuration
{
    public record RedisCacheSettings
    {
        public string? HostName { set; get; }

        public int Port { set; get; }

        public string? InstanceName { set; get; }
    }
}
