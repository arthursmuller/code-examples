namespace ExternalEntities.Infraestructure.Providers.RabbitMq
{
    public class RmqConfiguration
    {
        public string BusinessCreatedRoutingKey { get; set; }
        public string BusinessUpdatedRoutingKey { get; set; }
        public string UserCreatedRoutingKey { get; set; }
        public string UserUpdatedCpfRoutingKey { get; set; }
        public string BusinessCreatedExternalEntitiesQueueName { get; set; }
        public string BusinessUpdatedExternalEntitiesQueueName { get; set; }
        public string UserCreatedExternalEntitiesQueueName { get; set; }
        public string UserUpdatedCpfExternalEntitiesQueueName { get; set; }
    }
}