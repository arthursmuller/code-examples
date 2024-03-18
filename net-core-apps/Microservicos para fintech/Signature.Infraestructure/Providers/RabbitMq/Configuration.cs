namespace Signature.Infraestructure.Providers.RabbitMq
{
    public class RmqConfiguration
    {
        public string SignatureCertificateCreatedRoutingKey { get; set; }
        public string SignatureCreateRoutingKey { get; set; }
        public string SignatureCreateQueueName { get; set; }
        public string SignatureSignDocumentRoutingKey { get; set; }
        public string SignatureSignDocumentQueueName { get; set; }
        public string SignatureOverrideRoutingKey { get; set; }
        public string SignatureOverrideQueueName { get; set; }
        public string SignatureDeleteRoutingKey { get; set; }
        public string SignatureDeleteQueueName { get; set; }
    }
}
