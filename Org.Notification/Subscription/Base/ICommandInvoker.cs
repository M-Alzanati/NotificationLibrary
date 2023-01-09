namespace Org.Notification.Subscription.Base
{
    /// <summary>
    /// Invoking a specific command with a specific message
    /// </summary>
    public interface ICommandInvoker
    {
        /// <summary>
        /// Submit a command to be executed
        /// </summary>
        /// <param name="command"></param>
        /// <param name="message"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task SubmitAsync(ICommand command, object message, CancellationToken cancellationToken);
    }
}
