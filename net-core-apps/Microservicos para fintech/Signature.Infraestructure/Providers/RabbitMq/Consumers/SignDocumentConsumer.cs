using MessageBrokerRabbitMq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;
using Signature.Domain.Services;

namespace Signature.Infraestructure.Providers.RabbitMq.Consumers
{
    public class SignDocumentConsumer : MessageBrokerRabbitMQ
    {
        private readonly ILogger<SignDocumentConsumer> _logger;
        private readonly IServiceProvider _services;

        public SignDocumentConsumer(
            IServiceProvider services,
            string rmqExchangeName,
            string routingKey,
            string queueName,
            int retryCount,
            ILogger<SignDocumentConsumer> logger) : base(services, rmqExchangeName, routingKey, queueName, retryCount)
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
                    var userId = taskMessage.Value<int>("UserId");
                    var productTypeId = taskMessage.Value<int>("ProductTypeId");
                    var productRecordId = taskMessage.Value<int>("ProductRecordId");
                    var documentUrl = taskMessage.Value<string>("DocumentUrl");
                    var userIpAddress = taskMessage.Value<string>("UserIpAddress");
                    var longitude = taskMessage.Value<string>("Longitude");
                    var latitude = taskMessage.Value<string>("Latitude");
                    var userPictureUrl = taskMessage.Value<string>("UserPictureUrl");
                    
                    using (var scope = _services.CreateScope())
                    {
                        var signatureService = scope.ServiceProvider.GetRequiredService<ISignatureService>();
                        var signed = await signatureService.SignDocument(
                            userId,
                            productTypeId,
                            productRecordId,
                            documentUrl,
                            userPictureUrl,
                            longitude,
                            latitude,
                            userIpAddress);

                        _logger.LogInformation(" *************** Success Processing AddSignatureConsumer - signed: {signed} userId: {userId} ***************  ", signed, userId);
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
