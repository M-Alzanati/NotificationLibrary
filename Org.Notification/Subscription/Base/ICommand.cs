namespace Org.Notification.Subscription.Base
{
    public interface ICommand
    {
        Task ExecuteAsync(object message);

        Task RedoAsync(object message);
    }
}
