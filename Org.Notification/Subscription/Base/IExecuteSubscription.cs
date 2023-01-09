namespace Org.Notification.Subscription.Base
{
    public interface IExecuteSubscription
    {
        void DoSubscription(CancellationToken cancellationToken = default);
    }
}
