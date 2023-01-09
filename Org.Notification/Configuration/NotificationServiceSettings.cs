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
    public static class NotificationServiceSettings
    {
        public static IServiceCollection AddNotificationSettings(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IProducerRegistry, ProducerRegistry>();
            serviceCollection.AddSingleton<IExecuteSubscription, ExecuteSubscription>();
            serviceCollection.AddSingleton<ICommandInvoker, CommandInvoker>();

            serviceCollection.AddTransient<INotificationPublisher, NotificationPublisher>();
            serviceCollection.AddScoped<IEmailService, EmailService>();
            serviceCollection.AddScoped<ISmsService, SmsService>();

            return serviceCollection;
        }

        public static IServiceCollection WithSubscription(this IServiceCollection serviceCollection)
        {
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var worker = serviceProvider.GetRequiredService<IExecuteSubscription>();

            worker.DoSubscription();
            return serviceCollection;
        }

        public static IServiceCollection AddRabbitMqProducer(this IServiceCollection serviceCollection, ConfigurationManager configuration)
        {
            serviceCollection.AddSingleton<IMessageProducer, RabbitMqProducer>();
            serviceCollection.Configure<RabbitMqSettings>(configuration.GetSection(nameof(RabbitMqSettings)));
            return serviceCollection;
        }

        public static IServiceCollection AddRedisCache(this IServiceCollection serviceCollection, ConfigurationManager configuration)
        {
            serviceCollection.Configure<RedisCacheSettings>(configuration.GetSection(nameof(RedisCacheSettings)));
            
            serviceCollection.AddStackExchangeRedisCache(options =>
            {
                var redisSettings = configuration.Get<RedisCacheSettings>();
                options.Configuration = $"{redisSettings?.HostName ?? "localhost"}:{redisSettings?.Port ?? 6379}";
                options.InstanceName = redisSettings?.InstanceName ?? string.Empty;
            });

            return serviceCollection;
        }

        public static IServiceCollection AddSmsSubscription(this IServiceCollection serviceCollection, string? name = null)
        {
            RegisterPublisher<SmsPublisher, SmsCommand>(serviceCollection, name ?? "sms");
            return serviceCollection;
        }

        public static IServiceCollection AddEmailSubscription(this IServiceCollection serviceCollection, string? name = null)
        {
            RegisterPublisher<EmailPublisher, EmailCommand>(serviceCollection, name ?? "email");
            return serviceCollection;
        }   

        public static IServiceCollection AddCustomSubscription<TPImplementation, TWImplementation>(this IServiceCollection serviceCollection, string name)
            where TPImplementation : class, IPublisher, new()
            where TWImplementation : class, ICommand, new()
        {
            RegisterPublisher<TPImplementation, TWImplementation>(serviceCollection, name);
            return serviceCollection;
        }

        private static void RegisterPublisher<TPImplementation, TWImplementation>(IServiceCollection serviceCollection, string name, bool overrideDefault = false)
            where TPImplementation :  class, IPublisher
            where TWImplementation :  class, ICommand
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
