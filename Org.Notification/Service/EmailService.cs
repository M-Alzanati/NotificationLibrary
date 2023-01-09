using Org.Notification.Service.Interface;

namespace Org.Notification.Service
{
    public class EmailService : IEmailService
    {
        public Task SendEmailAsync(string? email, string? body)
        {
            // Concrete implementation for sending an email using any 3rd party or working with custom smtp settings
            return Task.CompletedTask;
        }
    }
}
