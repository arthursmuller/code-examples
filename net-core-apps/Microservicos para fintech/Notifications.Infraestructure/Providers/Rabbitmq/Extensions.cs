using MessageBrokerCore.Abstractions;
using MessageBrokerRabbitMq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Notifications.Infraestructure.Providers.RabbitMq.Consumers;
using System;
using System.Linq;

namespace Notifications.Infraestructure.Providers.RabbitMq
{
    public static class RabbitMqExtensions
    {
        public static IServiceCollection ConfigureRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            var retryCount = 5;
            ConfigureRabbitMqVariables(services, configuration);
            RabbitMqBrokerExtensions.ConfigureRabbitMqConnection(services, configuration,  retryCount);
            ConfigureRabbitMqConsumer(services, configuration, retryCount);

            return services;
        }
        private static IServiceCollection ConfigureRabbitMqConsumer(IServiceCollection services, IConfiguration configuration, int retryCount)
        {
            var config = services.BuildServiceProvider().GetRequiredService<RmqConfiguration>();
            var rmqExchangeName = GetSetting("RMQ_EXCHANGENAME", configuration);

            services.AddSingleton<IMessageBroker, BusinessCreatedConsumer>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<BusinessCreatedConsumer>>();
                return new BusinessCreatedConsumer(sp, rmqExchangeName, config.BusinessCreatedRoutingKey, config.BusinessCreatedNotificationQueueName, retryCount, logger);
            });
            services.AddSingleton<IMessageBroker, BusinessUpdatedConsumer>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<BusinessUpdatedConsumer>>();
                return new BusinessUpdatedConsumer(sp, rmqExchangeName, config.BusinessUpdatedRoutingKey, config.BusinessUpdatedNotificationQueueName, retryCount, logger);
            });
            services.AddSingleton<IMessageBroker, UserCreatedConsumer>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<UserCreatedConsumer>>();
                return new UserCreatedConsumer(sp, rmqExchangeName, config.UserCreatedRoutingKey, config.UserCreatedNotificationQueueName, retryCount, logger);
            });
            services.AddSingleton<IMessageBroker, UserUpdatedConsumer>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<UserUpdatedConsumer>>();
                return new UserUpdatedConsumer(sp, rmqExchangeName, config.UserUpdatedRoutingKey, config.UserUpdatedNotificationQueueName, retryCount, logger);
            });

            services.AddSingleton<IMessageBroker, UserUpdatedEmailConsumer>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<UserUpdatedEmailConsumer>>();
                return new UserUpdatedEmailConsumer(sp, rmqExchangeName, config.AuthUserUpdatedEmailRoutingKey, config.AuthUserUpdatedEmailNotificationQueueName, retryCount, logger);
            });
            services.AddSingleton<IMessageBroker, CreateNotificationConsumer>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<CreateNotificationConsumer>>();
                return new CreateNotificationConsumer(sp, rmqExchangeName, config.NotificationAddRoutingKey, config.NotificationAddQueueName, retryCount, logger);
            });

            return services;
        }
        private static IServiceCollection ConfigureRabbitMqVariables(IServiceCollection services, IConfiguration configuration)
        {
            var config = new RmqConfiguration
            {
                BusinessCreatedRoutingKey = GetSetting("business_created", configuration),
                BusinessUpdatedRoutingKey = GetSetting("business_updated", configuration),
                UserCreatedRoutingKey = GetSetting("auth_user_created", configuration),
                UserUpdatedRoutingKey = GetSetting("user_updated", configuration),
                AuthUserUpdatedEmailRoutingKey = GetSetting("auth_user_updated_email", configuration),
                AuthUserUpdatedEmailNotificationQueueName = GetSetting("Notification_Auth_User_Email_Updated_Queue", configuration),
                BusinessCreatedNotificationQueueName = GetSetting("Notification_Business_Created_Queue", configuration),
                BusinessUpdatedNotificationQueueName = GetSetting("Notification_Business_Updated_Queue", configuration),
                UserCreatedNotificationQueueName = GetSetting("Notification_Auth_User_Created_Queue", configuration),
                UserUpdatedNotificationQueueName = GetSetting("Notification_Auth_User_Updated_Queue", configuration),
                NotificationAddRoutingKey = GetSetting("notification_add", configuration),
                NotificationAddQueueName = GetSetting("Notification_Add_Queue", configuration),
            };

            services.AddSingleton(config);

            return services;
        }

        public static void ConfigureMessageBroker(IApplicationBuilder app)
        {
            var services = app.ApplicationServices.GetServices<IMessageBroker>();
            foreach (var service in services.Where(e => e.GetType().Name.ToLower().Contains("consumer")))
            {
                var isScheduled = service.GetType().Name.ToLower().Contains("scheduled");
                if (!isScheduled)
                    service.Subscribe();
                if (isScheduled)
                    service.SubscribeScheduled();
            }
        }

        private static string GetSetting(string setting, IConfiguration configuration) =>
            Environment.GetEnvironmentVariable(setting) ?? configuration[setting];
    }
}