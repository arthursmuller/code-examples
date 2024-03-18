namespace Notifications.Domain.Dtos
{
    public class NotificationChannelDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }

        private NotificationChannelDto() { }
        public NotificationChannelDto(int? id, string name) 
        {
            Id = id;
            Name = name;
        }
    }
}
