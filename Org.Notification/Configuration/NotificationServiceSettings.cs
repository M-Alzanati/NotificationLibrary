using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Org.Notification.Producer;
using Org.Notification.Producer.Interface;
using Org.Notification.Publisher;
using Org.Notification.Publisher.Interface;
using Org.Notification.Service;
using Org.Notification.Service.Interface;
using Org.Notification.Subscription;
using Org.Notification.Subscription.Base;
using Org.Notification.Subscription.Command;

namespace Org.Notification.Configuration
{
    /// <summary>
    /// Notification Service settings
    /// </summary>
    public static class NotificationServiceSettings
    {
        /// <summary>
        /// Registering required services
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <returns></returns>
        public static IServiceCollection AddNotificationSettings(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IProducerRegistry, ProducerRegistry>();
            serviceCollection.AddSingleton<ICommandInvoker, CommandInvoker>();

            serviceCollection.AddScoped<IExecuteSubscription, ExecuteSubscription>();
            serviceCollection.AddScoped<INotificationsPublisher, NotificationsPublisher>();
            serviceCollection.AddScoped<IEmailService, EmailService>();
            serviceCollection.AddScoped<ISmsService, SmsService>();

            return serviceCollection;
        }

        /// <summary>
        /// Register all workers
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <returns></returns>
        public static IServiceCollection WithSubscription(this IServiceCollection serviceCollection)
        {
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var worker = serviceProvider.GetRequiredService<IExecuteSubscription>();

            worker.DoSubscription();
            return serviceCollection;
        }

        /// <summary>
        /// Add RabbitMQ support
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddRabbitMqProducer(this IServiceCollection serviceCollection, ConfigurationManager configuration)
        {
            serviceCollection.AddScoped<IMessageProducer, RabbitMqProducer>();
            serviceCollection.Configure<RabbitMqSettings>(configuration.GetSection(nameof(RabbitMqSettings)));
            return serviceCollection;
        }

        /// <summary>
        /// Add redis caching support 
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddRedisCache(this IServiceCollection serviceCollection, ConfigurationManager configuration)
        {
            serviceCollection.Configure<RedisCacheSettings>(configuration.GetSection(nameof(RedisCacheSettings)));

            serviceCollection.AddStackExchangeRedisCache(options =>
            {
                var redisSettings = configuration.Get<RedisCacheSettings>();
                options.Configuration = $"{redisSettings?.HostName ?? "localhost"}:{redisSettings?.Port ?? 6793}";
                options.InstanceName = redisSettings?.InstanceName ?? string.Empty;
            });

            return serviceCollection;
        }

        /// <summary>
        /// Registering sms publisher and worker
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IServiceCollection AddSmsSubscription(this IServiceCollection serviceCollection, string? name = null)
        {
            RegisterPublisher<SmsPublisher, SmsCommand>(serviceCollection, name ?? "sms");
            return serviceCollection;
        }

        /// <summary>
        /// Registering email publisher and worker
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IServiceCollection AddEmailSubscription(this IServiceCollection serviceCollection, string? name = null)
        {
            RegisterPublisher<EmailPublisher, EmailCommand>(serviceCollection, name ?? "email");
            return serviceCollection;
        }

        /// <summary>
        /// Registering custom publisher and custom worker
        /// </summary>
        /// <typeparam name="TPImplementation"></typeparam>
        /// <typeparam name="TWImplementation"></typeparam>
        /// <param name="serviceCollection"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IServiceCollection AddCustomSubscription<TPImplementation, TWImplementation>(this IServiceCollection serviceCollection, string name)
            where TPImplementation : class, IPublisher, new()
            where TWImplementation : class, ICommand, new()
        {
            RegisterPublisher<TPImplementation, TWImplementation>(serviceCollection, name);
            return serviceCollection;
        }

        private static void RegisterPublisher<TPImplementation, TWImplementation>(IServiceCollection serviceCollection, string name, bool overrideDefault = false)
            where TPImplementation : class, IPublisher
            where TWImplementation : class, ICommand
        {
            serviceCollection.AddScoped<TPImplementation>();
            serviceCollection.AddScoped<TWImplementation>();
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var publisherFactory = serviceProvider.GetRequiredService<IProducerRegistry>();
            var publisher = serviceProvider.GetRequiredService<TPImplementation>();
            var worker = serviceProvider.GetRequiredService<TWImplementation>();

            publisherFactory.RegisterProducer(publisher, worker, name, overrideDefault);
        }
    }
}
