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

        public override async Task ExecuteAsync(object message, CancellationToken cancellationToken)
        {
            var myMessage = ParseMessage(message);
            await _smsService.SendEmailAsync(myMessage.RecipientEmail, myMessage.Body, cancellationToken);
        }

        public override async Task RedoAsync(object message, CancellationToken cancellationToken)
        {
            var myMessage = ParseMessage(message);
            await _smsService.SendEmailAsync(myMessage.RecipientEmail, myMessage.Body, cancellationToken);
        }
    }
}
