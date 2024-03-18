using MessageBrokerCore.Abstractions;
using MessageBrokerRabbitMq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ExternalEntities.Infraestructure.Providers.RabbitMq.Consumers;
using System;
using System.Linq;

namespace ExternalEntities.Infraestructure.Providers.RabbitMq
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
                return new BusinessCreatedConsumer(sp, rmqExchangeName, config.BusinessCreatedRoutingKey, config.BusinessCreatedExternalEntitiesQueueName, retryCount, logger);
            });
            services.AddSingleton<IMessageBroker, BusinessUpdatedConsumer>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<BusinessUpdatedConsumer>>();
                return new BusinessUpdatedConsumer(sp, rmqExchangeName, config.BusinessUpdatedRoutingKey, config.BusinessUpdatedExternalEntitiesQueueName, retryCount, logger);
            });
            services.AddSingleton<IMessageBroker, UserCreatedConsumer>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<UserCreatedConsumer>>();
                return new UserCreatedConsumer(sp, rmqExchangeName, config.UserCreatedRoutingKey, config.UserCreatedExternalEntitiesQueueName, retryCount, logger);
            });
            services.AddSingleton<IMessageBroker, UserUpdatedConsumer>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<UserUpdatedConsumer>>();
                return new UserUpdatedConsumer(sp, rmqExchangeName, config.UserUpdatedCpfRoutingKey, config.UserUpdatedCpfExternalEntitiesQueueName, retryCount, logger);
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
                UserUpdatedCpfRoutingKey = GetSetting("auth_user_updated_cpf", configuration),
                BusinessCreatedExternalEntitiesQueueName = GetSetting("ExternalEntities_Business_Created_Queue", configuration),
                BusinessUpdatedExternalEntitiesQueueName = GetSetting("ExternalEntities_Business_Updated_Queue", configuration),
                UserCreatedExternalEntitiesQueueName = GetSetting("ExternalEntities_Auth_User_Created_Queue", configuration),
                UserUpdatedCpfExternalEntitiesQueueName = GetSetting("ExternalEntities_Auth_User_Updated_Cpf_Queue", configuration),
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