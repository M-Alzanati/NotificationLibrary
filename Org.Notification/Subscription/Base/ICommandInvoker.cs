namespace Org.Notification.Subscription.Base
{
    public interface ICommandInvoker
    {
        void Submit(ICommand command, object message, CancellationToken cancellationToken);
    }
}
