using Org.Notification.Publisher;
using Org.Notification.Publisher.Interface;
using Org.Notification.Subscription.Base;

namespace Org.Notification.Producer.Interface
{
    public interface IProducerRegistry : IDisposable
    {
        string GetPublisherName<TService>() where TService : IPublisher;

        string GetWorkerName<TService>() where TService : ICommand;

        IPublisher GetPublisher<TService>() where TService : IPublisher;

        ICommand GetWorker<TService>() where TService : ICommand;

        IEnumerable<KeyValuePair<string, ICommand>> GetWorkers();

        void RegisterProducer(IPublisher publisher, ICommand worker, string name, bool overrideDefault = false);
    }
}
