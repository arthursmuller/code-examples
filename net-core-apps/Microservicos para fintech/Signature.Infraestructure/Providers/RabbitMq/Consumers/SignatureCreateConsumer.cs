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
    public class SignatureCreateConsumer : MessageBrokerRabbitMQ
    {
        private readonly ILogger<SignatureCreateConsumer> _logger;
        private readonly IServiceProvider _services;

        public SignatureCreateConsumer(
            IServiceProvider services,
            string rmqExchangeName,
            string routingKey,
            string queueName,
            int retryCount,
            ILogger<SignatureCreateConsumer> logger) : base(services, rmqExchangeName, routingKey, queueName, retryCount)
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
                    var documentId = Convert.ToInt32(taskMessage.Value<string>("DocumentId"));
                    var userId = Convert.ToInt32(taskMessage.Value<string>("UserId"));
                    var productTypeId = Convert.ToInt32(taskMessage.Value<string>("ProductTypeId"));
                    var productRecordId = Convert.ToInt32(taskMessage.Value<string>("ProductRecordId"));
                    var documentCreatedDate = DtHelper.FromString(taskMessage.Value<string>("DocumentCreatedDate"));
                    var documentUrl = taskMessage.Value<string>("DocumentUrl");
                    var signerIdentification = taskMessage.Value<string>("SignerIdentification");
                    var signerName = taskMessage.Value<string>("SignerName");
                    var signerCellphone = taskMessage.Value<string>("SignerCellphone");
                    var signerEmail = taskMessage.Value<string>("SignerEmail");
                    var documentNumber = taskMessage.Value<string>("DocumentNumber");
                    var userIpAddress = taskMessage.Value<string>("UserIpAddress");
                    var longitude = taskMessage.Value<string>("Longitude");
                    var latitude = taskMessage.Value<string>("Latitude");
                    var signaturePictureBase64string = taskMessage.Value<string>("SignaturePictureBase64string");
                    var base64SignatureString = taskMessage.Value<string>("Base64SignatureString");
                    var routingKey = taskMessage.Value<string>("RoutingKey");
                    var documentExtension = taskMessage.Value<string>("DocumentFileExtension");
                    byte[] document = taskMessage.SelectToken("Document").ToObject<byte[]>();

                    bool? saveSignatureOnly = null;
                    bool saveSignatureOnlyHelper;
  
                    if (Boolean.TryParse(taskMessage.Value<string>("SaveSignatureOnly"), out saveSignatureOnlyHelper))
                        saveSignatureOnly = saveSignatureOnlyHelper;

                    using (var scope = _services.CreateScope())
                    {
                        var signatureService = scope.ServiceProvider.GetRequiredService<ISignatureService>();
                        var rabbitMqProvider = scope.ServiceProvider.GetRequiredService<IRabbitMqProvider>();
                        var newSignature = await signatureService.GenerateSignature(
                            documentId,
                            userId,
                            productTypeId,
                            productRecordId,
                            documentCreatedDate ?? DateTime.Now,
                            documentUrl,
                            document,
                            documentExtension,
                            signerIdentification,
                            signerName,
                            signerCellphone,
                            signerEmail,
                            documentNumber,
                            userIpAddress,
                            longitude,
                            latitude,
                            signaturePictureBase64string,
                            base64SignatureString,
                            saveSignatureOnly);

                        rabbitMqProvider.CertificateCreated(null, $"{productRecordId}", $"{newSignature.Signature.Id}", routingKey);
                        _logger.LogInformation(" *************** Success Processing AddSignatureConsumer - newSignature: {newSignature} userId: {userId} ***************  ", newSignature.Signature.Id, userId);
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
