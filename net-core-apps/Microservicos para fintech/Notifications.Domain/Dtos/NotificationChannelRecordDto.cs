namespace Notifications.Domain.Dtos
{
    public class NotificationChannelRecordDto
    {
        public NotificationChannelDto Channel { get; set; }
        private NotificationChannelRecordDto() { }

        public NotificationChannelRecordDto(NotificationChannelDto channel)  
        {
            Channel = channel;
        }
    }
}
