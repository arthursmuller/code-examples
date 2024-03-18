namespace Notifications.Domain.Dtos
{
    public class NotificationTypeDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }

        private NotificationTypeDto() { }
        public NotificationTypeDto(int? id, string name)  {
            Id = id;
            Name = name;
        }
    }
}
