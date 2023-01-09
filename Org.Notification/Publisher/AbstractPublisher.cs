using Org.Notification.Core;
using Org.Notification.Producer.Interface;
using Org.Notification.Publisher.Interface;

namespace Org.Notification.Publisher
{
    public abstract class AbstractPublisher<TMessage> : IPublisher where TMessage : IMessageDto
    {
        protected IMessageProducer MessageProducer;

        protected IProducerRegistry PublisherFactory;

        protected AbstractPublisher(IMessageProducer messageProducer, IProducerRegistry publisherFactory)
        {
            MessageProducer = messageProducer;
            PublisherFactory = publisherFactory;
        }

        protected virtual IMessageDto? DecorateMessageDto(NotificationMessage message)
        {
            var temp = message.DetailMessage.SingleOrDefault(e => e?.GetType() == typeof(TMessage));
            if (temp == null) return null;

            temp.Id = message.Id;
            return temp;
        }

        public virtual async Task<IEnumerable<Guid>> NotifyAsync(NotificationMessage message, CancellationToken cancellationToken)
        {
            var messageDto = DecorateMessageDto(message);
            await MessageProducer.SendMessageAsync(GetPublisherName(), messageDto ?? throw new InvalidOperationException(), cancellationToken);
            return new List<Guid> { message.Id };
        }

        public abstract string GetPublisherName();
    }
}
