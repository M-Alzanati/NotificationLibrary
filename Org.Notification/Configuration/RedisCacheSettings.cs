namespace Org.Notification.Configuration
{
    /// <summary>
    /// Redis cache settings
    /// </summary>
    public record RedisCacheSettings
    {
        public string? HostName { set; get; }

        public int? Port { set; get; }

        public string? InstanceName { set; get; }
    }
}
