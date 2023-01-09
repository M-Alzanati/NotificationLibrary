using Org.Notification.Message.Interface;

namespace Org.Notification.Subscription.Base
{
    /// <summary>
    /// Implementation for command pattern
    /// This is the command part
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Get message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        IMessageDto GetMessage(object message);

        /// <summary>
        /// Execute command
        /// </summary>
        /// <param name="message"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task ExecuteAsync(object message, CancellationToken cancellationToken);

        /// <summary>
        /// ReExecute the command if it fails
        /// </summary>
        /// <param name="message"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task RedoAsync(object message, CancellationToken cancellationToken);

        /// <summary>
        /// Get message as a string
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        string GetMessageAsString(object message);

        /// <summary>
        /// Command name
        /// </summary>
        string Name { get; }
    }
}
