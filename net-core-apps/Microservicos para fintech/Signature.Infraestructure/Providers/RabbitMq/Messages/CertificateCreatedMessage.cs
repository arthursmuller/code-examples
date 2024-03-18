using MessageBrokerCore.Messages;
using Newtonsoft.Json;

namespace Signature.Infraestructure.Providers.RabbitMq.Messages
{
    public class CertificateCreatedMessage : IntegrationMessage
    {

        [JsonProperty]
        public string Base64Certificate { get; set; }

        [JsonProperty]
        public string ExternalId { get; set; }
        
        [JsonProperty]
        public string SignatureId { get; set; }

        public CertificateCreatedMessage(string base64Certificate, string externalId, string signatureId)
        {
            Base64Certificate = base64Certificate;
            ExternalId = externalId;
            SignatureId = signatureId;
        }
    }
}
