using System.Text.Json;
using Org.Notification.Subscription.Base;

namespace Org.Notification.Subscription.Command
{
    public abstract class AbstractCommand<TMessage> : ICommand where TMessage : class, IMessageDto
    {
        public virtual TMessage ParseMessage(object message)
        {
            var objAsString = JsonSerializer.Serialize(message);
            var obj = JsonSerializer.Deserialize<TMessage>(objAsString);
            return obj ?? throw new InvalidOperationException();
        }

        public abstract Task ExecuteAsync(object message);

        public abstract Task RedoAsync(object message);
    }
}
