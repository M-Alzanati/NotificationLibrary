namespace Org.Notification.Service.Interface
{
    /// <summary>
    /// Implementation for sms service
    /// </summary>
    public interface ISmsService
    {
        /// <summary>
        /// Sending sms
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="body"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task SendSmsAsync(string? mobile, string? body, CancellationToken cancellationToken);
    }
}
