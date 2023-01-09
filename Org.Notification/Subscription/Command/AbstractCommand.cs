using System.Text.Json;
using Org.Notification.Message.Interface;
using Org.Notification.Subscription.Base;

namespace Org.Notification.Subscription.Command
{
    /// <summary>
    /// Base for any command
    /// </summary>
    /// <typeparam name="TMessage"></typeparam>
    public abstract class AbstractCommand<TMessage> : ICommand where TMessage : class, IMessageDto
    {
        public virtual TMessage ParseMessage(object message)
        {
            var obj = JsonSerializer.Deserialize<TMessage>(GetMessageAsString(message));
            return obj ?? throw new InvalidOperationException();
        }

        public string GetMessageAsString(object message)
        {
            return JsonSerializer.Serialize(message);
        }

        public IMessageDto GetMessage(object message)
        {
            return ParseMessage(message);
        }

        public abstract Task ExecuteAsync(object message, CancellationToken cancellationToken);

        public abstract Task RedoAsync(object message, CancellationToken cancellationToken);

        public virtual string Name => typeof(TMessage).Name;
    }
}
