using Org.Notification.Service.Interface;

namespace Org.Notification.Subscription.Command
{
    public class SmsCommand : AbstractCommand<SmsMessageDto>
    {
        private readonly ISmsService _smsService;

        public SmsCommand(ISmsService smsService)
        {
            _smsService = smsService;
        }

        public override async Task ExecuteAsync(object message, CancellationToken cancellationToken)
        {
            var myMessage = ParseMessage(message);
            await _smsService.SendSmsAsync(myMessage.RecipientPhone, myMessage.Body, cancellationToken);
        }

        public override async Task RedoAsync(object message, CancellationToken cancellationToken)
        {
            var myMessage = ParseMessage(message);
            await _smsService.SendSmsAsync(myMessage.RecipientPhone, myMessage.Body, cancellationToken);
        }
    }
}
