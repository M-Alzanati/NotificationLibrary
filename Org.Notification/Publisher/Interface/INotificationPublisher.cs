namespace Org.Notification.Publisher.Interface
{
    /// <summary>
    /// Implementation for the publisher observable
    /// </summary>
    public interface INotificationsPublisher : IPublisher
    {
        /// <summary>
        /// Add publisher to be observed
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        INotificationsPublisher Subscribe<TService>() where TService : IPublisher;

        /// <summary>
        /// Remove publisher to be observed
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        INotificationsPublisher UnSubscribe<TService>() where TService : IPublisher;

        /// <summary>
        /// Remove all publishers
        /// </summary>
        /// <returns></returns>
        INotificationsPublisher UnSubscribeAll();
    }
}
