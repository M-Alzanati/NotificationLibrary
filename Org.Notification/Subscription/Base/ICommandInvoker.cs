namespace Org.Notification.Subscription.Base
{
    public interface ICommandInvoker
    {
        Task SubmitAsync(ICommand command, object message, CancellationToken cancellationToken);
    }
}
