namespace Org.Notification.Subscription.Base
{
    public interface ICommand
    {
        IMessageDto GetMessage(object message);

        Task ExecuteAsync(object message, CancellationToken cancellationToken);

        Task RedoAsync(object message, CancellationToken cancellationToken);

        string GetMessageAsString(object message);

        string Name { get; }
    }
}
