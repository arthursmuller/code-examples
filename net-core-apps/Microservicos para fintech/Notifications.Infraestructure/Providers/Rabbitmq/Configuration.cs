namespace Notifications.Infraestructure.Providers.RabbitMq
{
    public class RmqConfiguration
    {
        public string BusinessCreatedRoutingKey { get; set; }
        public string BusinessUpdatedRoutingKey { get; set; }
        public string UserCreatedRoutingKey { get; set; }
        public string UserUpdatedRoutingKey { get; set; }
        public string BusinessCreatedNotificationQueueName { get; set; }
        public string BusinessUpdatedNotificationQueueName { get; set; }
        public string UserCreatedNotificationQueueName { get; set; }
        public string UserUpdatedNotificationQueueName { get; set; }
        public string NotificationAddRoutingKey { get; set; }
        public string NotificationAddQueueName { get; set; }
        public string AuthUserUpdatedEmailRoutingKey { get; set; }
        public string AuthUserUpdatedEmailNotificationQueueName { get; set; }
    }
}