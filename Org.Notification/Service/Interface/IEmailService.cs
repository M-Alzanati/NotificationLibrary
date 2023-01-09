namespace Org.Notification.Service.Interface
{
    public interface IEmailService
    {
        Task SendEmailAsync(string? email, string? body, CancellationToken cancellationToken);
    }
}
