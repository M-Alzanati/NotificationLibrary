using Org.Notification.Service.Interface;

namespace Org.Notification.Service
{
    public class SmsService : ISmsService
    {
        public Task SendSmsAsync(string? mobile, string? body, CancellationToken cancellationToken)
        {
            // Concrete implementation for sending an sms using any 3rd party or working with custom apn settings
            return Task.CompletedTask;
        }
    }
}
