using MessageBrokerRabbitMq;
using System;
using Signature.Infraestructure.Providers.RabbitMq.Messages;
using Signature.Infraestructure.Providers.RabbitMq.Abstractions;

namespace Signature.Infraestructure.Providers.RabbitMq
{
    public class RabbitMqProvider : MessageBrokerRabbitMQ, IRabbitMqProvider
    {
        private RmqConfiguration Configuration;
        public RabbitMqProvider(
            IServiceProvider services,
            RmqConfiguration configuration,
            string rmqExchangeName,
            int retryCount) : base(services, rmqExchangeName, null, null, retryCount)
            => Configuration = configuration;

        public Guid CertificateCreated(string base64Certificate, string externalId, string signatureId, string routingKey)
         => Publish(new CertificateCreatedMessage(base64Certificate, externalId, signatureId), routingKey ?? Configuration.SignatureCertificateCreatedRoutingKey);
    }
}
