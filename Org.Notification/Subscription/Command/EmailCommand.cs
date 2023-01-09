using Org.Notification.Service.Interface;

namespace Org.Notification.Subscription.Command
{
    public class EmailCommand : AbstractCommand<EmailMessageDto>
    {
        private readonly IEmailService _smsService;

        public EmailCommand(IEmailService smsService)
        {
            _smsService = smsService;
        }

        public override async Task ExecuteAsync(object message)
        {
            var myMessage = ParseMessage(message);
            await _smsService.SendEmailAsync(myMessage.RecipientEmail, myMessage.Body);
        }

        public override async Task RedoAsync(object message)
        {
            var myMessage = ParseMessage(message);
            await _smsService.SendEmailAsync(myMessage.RecipientEmail, myMessage.Body);
        }
    }
}
