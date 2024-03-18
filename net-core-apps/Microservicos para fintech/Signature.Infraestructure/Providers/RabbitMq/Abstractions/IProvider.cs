using System;

namespace Signature.Infraestructure.Providers.RabbitMq.Abstractions
{
    public interface IRabbitMqProvider
    {
        Guid CertificateCreated(string base64Certificate, string externalId, string signatureId, string routingKey);
    }
}
