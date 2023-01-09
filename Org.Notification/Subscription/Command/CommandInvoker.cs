using Org.Notification.Subscription.Base;

namespace Org.Notification.Subscription.Command
{
    public class CommandInvoker : ICommandInvoker
    {
        public void Submit(ICommand command, object message, CancellationToken cancellationToken)
        {
            command.ExecuteAsync(message).Wait(cancellationToken);
        }
    }
}
