namespace Org.Notification.Service.Interface
{
    /// <summary>
    /// Implementation for email service
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Sending email
        /// </summary>
        /// <param name="email"></param>
        /// <param name="body"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task SendEmailAsync(string? email, string? body, CancellationToken cancellationToken);
    }
}
