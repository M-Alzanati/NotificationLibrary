using Microsoft.Extensions.Caching.Distributed;
using Org.Notification.Subscription.Base;

namespace Org.Notification.Subscription.Command
{
    public class CommandInvoker : ICommandInvoker
    {
        protected readonly IDistributedCache DistributedCache;

        public CommandInvoker(IDistributedCache distributedCache)
        {
            DistributedCache = distributedCache;
        }

        public virtual async Task SubmitAsync(ICommand command, object message, CancellationToken cancellationToken)
        {
            try
            {
                var key = HashMessageKey(command, message);
                var redisKey = await DistributedCache.GetStringAsync(key, cancellationToken);

                if (string.IsNullOrEmpty(redisKey))
                {
                    await DistributedCache.SetStringAsync(key, command.GetMessageAsString(message), token: cancellationToken);
                    await command.ExecuteAsync(message, cancellationToken);
                }
                else
                {
                    // Means that we are done
                    // Also we can add more logic to retry failed commands
                    await DistributedCache.RemoveAsync(key, cancellationToken);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private string HashMessageKey(ICommand command, object message)
        {
            var messageWithId = command.GetMessage(message);
            return $"{messageWithId.Id}_{command.Name}";
        }
    }
}
