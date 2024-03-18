using MessageBrokerRabbitMq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;
using Signature.Domain.Services;
using Signature.Infraestructure.Providers.RabbitMq.Abstractions;

namespace Signature.Infraestructure.Providers.RabbitMq.Consumers
{
    public class SignatureDeleteConsumer : MessageBrokerRabbitMQ
    {
        private readonly ILogger<SignatureDeleteConsumer> _logger;
        private readonly IServiceProvider _services;

        public SignatureDeleteConsumer(
            IServiceProvider services,
            string rmqExchangeName,
            string routingKey,
            string queueName,
            int retryCount,
            ILogger<SignatureDeleteConsumer> logger) : base(services, rmqExchangeName, routingKey, queueName, retryCount)
        {
            _services = services;
            base.CreateConnection();
            base.RegisterQueue();
            _logger = logger;
        }

        public override async Task ProcessMessageAsync(string message)
        {
            var taskMessage = JToken.Parse(message);

            if (taskMessage != null)
            {
                try
                {
                    var signatureId = taskMessage.Value<int>("SignatureId");

                    using (var scope = _services.CreateScope())
                    {
                        var signatureService = scope.ServiceProvider.GetRequiredService<ISignatureService>();
                        await signatureService.Delete(signatureId);

                        _logger.LogInformation(" *************** Success Processing SignatureDeleteConsumer - signatureId: {signatureId} ***************  ", signatureId);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogInformation(
                        "SignatureDeleteConsumer Message Process fail,error:{ex.Message},stackTrace:{ex.StackTrace},message:{message}",
                        ex.Message,
                        ex.StackTrace,
                        message);

                    _logger.LogError(-1, ex, "Message Process fail");

                    throw;
                }
            }
        }
    }
}
