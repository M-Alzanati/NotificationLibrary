namespace Org.Notification.Service.Interface
{
    public interface ISmsService
    {
        Task SendSmsAsync(string? mobile, string? body, CancellationToken cancellationToken);
    }
}
