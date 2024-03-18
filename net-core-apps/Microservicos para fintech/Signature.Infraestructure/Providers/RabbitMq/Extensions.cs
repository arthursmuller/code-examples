using MessageBrokerCore.Abstractions;
using MessageBrokerRabbitMq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Signature.Infraestructure.Providers.RabbitMq.Abstractions;
using Signature.Infraestructure.Providers.RabbitMq.Consumers;
using System;
using System.Linq;

namespace Signature.Infraestructure.Providers.RabbitMq
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

            services.AddSingleton<IRabbitMqProvider, RabbitMqProvider>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<RabbitMqProvider>>();
                return new RabbitMqProvider(sp, config, rmqExchangeName, retryCount);
            });

            services.AddSingleton<IMessageBroker, SignatureCreateConsumer>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<SignatureCreateConsumer>>();
                return new SignatureCreateConsumer(sp, rmqExchangeName, config.SignatureCreateRoutingKey, config.SignatureCreateQueueName, retryCount, logger);
            });

            services.AddSingleton<IMessageBroker, SignDocumentConsumer>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<SignDocumentConsumer>>();
                return new SignDocumentConsumer(sp, rmqExchangeName, config.SignatureSignDocumentRoutingKey, config.SignatureSignDocumentQueueName, retryCount, logger);
            });

            services.AddSingleton<IMessageBroker, SignDocumentConsumer>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<SignDocumentConsumer>>();
                return new SignDocumentConsumer(sp, rmqExchangeName, config.SignatureSignDocumentRoutingKey, config.SignatureSignDocumentQueueName, retryCount, logger);
            });

            services.AddSingleton<IMessageBroker, SignatureDeleteConsumer>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<SignatureDeleteConsumer>>();
                return new SignatureDeleteConsumer(sp, rmqExchangeName, config.SignatureDeleteRoutingKey, config.SignatureDeleteQueueName, retryCount, logger);
            });

            services.AddSingleton<IMessageBroker, SignatureOverrideConsumer>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<SignatureOverrideConsumer>>();
                return new SignatureOverrideConsumer(sp, rmqExchangeName, config.SignatureOverrideRoutingKey, config.SignatureOverrideQueueName, retryCount, logger);
            });


            return services;
        }
        private static IServiceCollection ConfigureRabbitMqVariables(IServiceCollection services, IConfiguration configuration)
        {
            var config = new RmqConfiguration
            {
                SignatureCertificateCreatedRoutingKey = GetSetting("signature_certificate_created", configuration),
                SignatureCreateRoutingKey = GetSetting("signature_create", configuration),
                SignatureCreateQueueName = GetSetting("Signature_Create_Queue", configuration),
                SignatureSignDocumentRoutingKey = GetSetting("signature_sign_document", configuration),
                SignatureSignDocumentQueueName = GetSetting("Signature_Sign_Document_Queue", configuration),
                SignatureOverrideRoutingKey = GetSetting("signature_override", configuration),
                SignatureOverrideQueueName = GetSetting("Signature_Override_Queue", configuration),
                SignatureDeleteRoutingKey = GetSetting("signature_delete", configuration),
                SignatureDeleteQueueName = GetSetting("Signature_Delete_Queue", configuration),
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