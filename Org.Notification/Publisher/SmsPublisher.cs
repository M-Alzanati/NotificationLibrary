using Org.Notification.Producer.Interface;

namespace Org.Notification.Publisher
{
    public class SmsPublisher : AbstractPublisher<SmsMessageDto>
    {
        public SmsPublisher(IMessageProducer producer, IProducerRegistry publisherFactory) 
            : base(producer, publisherFactory)
        {
        }

        public override string GetPublisherName()
        {
            return PublisherFactory.GetPublisherName<SmsPublisher>();
        }
    }
}
