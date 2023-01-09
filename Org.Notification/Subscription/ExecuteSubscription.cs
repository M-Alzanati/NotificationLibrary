﻿using Org.Notification.Producer.Interface;
using Org.Notification.Subscription.Base;

namespace Org.Notification.Subscription
{
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

        public void DoSubscription(CancellationToken cancellationToken)
        {
            foreach (var worker in _producerRegistry.GetWorkers())
            {
                _messageProducer.DoSubscription(worker.Key, message =>
                {
                    _workerInvoker.Submit(worker.Value, message, cancellationToken);
                });
            }
        }
    }
}