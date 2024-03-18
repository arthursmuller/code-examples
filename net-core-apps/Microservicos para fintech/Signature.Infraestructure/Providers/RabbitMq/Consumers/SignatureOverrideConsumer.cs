using MessageBrokerRabbitMq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;
using Signature.Domain.Services;
using Signature.Infraestructure.Providers.RabbitMq.Abstractions;
using BrDateTimeUtils;

namespace Signature.Infraestructure.Providers.RabbitMq.Consumers
{
    public class SignatureOverrideConsumer : MessageBrokerRabbitMQ
    {
        private readonly ILogger<SignatureOverrideConsumer> _logger;
        private readonly IServiceProvider _services;

        public SignatureOverrideConsumer(
            IServiceProvider services,
            string rmqExchangeName,
            string routingKey,
            string queueName,
            int retryCount,
            ILogger<SignatureOverrideConsumer> logger) : base(services, rmqExchangeName, routingKey, queueName, retryCount)
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
                    var signatureId = Convert.ToInt32(taskMessage.Value<string>("SignatureId"));
                    var documentNumber = taskMessage.Value<string>("DocumentNumber");
                    var signerName = taskMessage.Value<string>("SignerName");
                    byte[] document = taskMessage.SelectToken("Document").ToObject<byte[]>();

                    using (var scope = _services.CreateScope())
                    {
                        var signatureService = scope.ServiceProvider.GetRequiredService<ISignatureService>();
                        var rabbitMqProvider = scope.ServiceProvider.GetRequiredService<IRabbitMqProvider>();
                        var newSignature = await signatureService.Override(
                            signatureId,
                            document,
                            signerName,
                            documentNumber);

                        _logger.LogInformation(" *************** Success Processing SignatureOverrideConsumer - newSignature: {newSignature} ***************  ", newSignature.Signature.Id);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(-1, ex, "Message Process fail");

                    throw;
                }
            }
        }
    }
}
