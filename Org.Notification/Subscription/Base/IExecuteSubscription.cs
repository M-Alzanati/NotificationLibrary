namespace Org.Notification.Subscription.Base
{
    /// <summary>
    /// Subscribe to workers
    /// </summary>
    public interface IExecuteSubscription
    {
        /// <summary>
        /// Do subscription to workers according to message producer and registered workers
        /// </summary>
        /// <param name="cancellationToken"></param>
        void DoSubscription(CancellationToken cancellationToken = default);
    }
}
