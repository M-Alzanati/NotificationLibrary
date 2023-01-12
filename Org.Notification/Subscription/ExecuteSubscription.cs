using Org.Notification.Producer.Interface;
using Org.Notification.Subscription.Base;

namespace Org.Notification.Subscription
{
    /// <summary>
    /// Do subscription part, we can extend this implementation to support more than message producer
    /// </summary>
    public class ExecuteSubscription : IExecuteSubscription
    {
        private readonly IProducerRegistry _producerRegistry;
        private readonly IMessageProducer _messageProducer;
        private readonly ICommandInvoker _workerInvoker;

        public ExecuteSubscription(IMessageProducer messageProducer, IProducerRegistry subscriptionRegistry, ICommandInvoker workerInvoker)
        {
            _producerRegistry = subscriptionRegistry;
            _messageProducer = messageProducer;
            _workerInvoker = workerInvoker;
        }

        public void DoSubscription(CancellationToken cancellationToken = default)
        {
            foreach (var worker in _producerRegistry.GetWorkers())
            {
                _messageProducer.DoSubscription(worker.Key, async message =>
                {
                    if (message != null)
                    {
                        await _workerInvoker.SubmitAsync(worker.Value, message, cancellationToken);
                    }
                });
            }
        }
    }
}
