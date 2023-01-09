using Org.Notification.Producer.Interface;

namespace Org.Notification.Publisher
{
    public class EmailPublisher : AbstractPublisher<EmailMessageDto>
    {
        public EmailPublisher(IMessageProducer producer, IProducerRegistry publisherFactory)
            : base(producer, publisherFactory)
        {
        }

        public override string GetPublisherName()
        {
            return PublisherFactory.GetPublisherName<EmailPublisher>();
        }
    }
}
