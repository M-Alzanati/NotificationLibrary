using Microsoft.AspNetCore.Mvc;
using Org.Notification.Core;
using Org.Notification.Message;
using Org.Notification.Publisher;
using Org.Notification.Publisher.Interface;

namespace TestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SendNotificationController : ControllerBase
    {
        private readonly ILogger<SendNotificationController> _logger;
        private readonly INotificationPublisher _notificationPublisher;

        public SendNotificationController(ILogger<SendNotificationController> logger, INotificationPublisher notificationPublisher)
        {
            _logger = logger;
            _notificationPublisher = notificationPublisher;

            notificationPublisher
                .Subscribe<SmsPublisher>()
                .Subscribe<EmailPublisher>();
        }

        [HttpPost(Name = "SendNotification")]
        public async Task<IEnumerable<Guid>> Post()
        {
            var result = await _notificationPublisher.NotifyAsync(new NotificationMessage
            {
                DetailMessage =
                {
                    new SmsMessageDto{ Body = "Hello Sms!", RecipientPhone = "921212121212" },
                    new EmailMessageDto { Body = "Hello Email!", RecipientEmail = "h93_b@gmail" }
                }
            });

            return result;
        }
    }
}