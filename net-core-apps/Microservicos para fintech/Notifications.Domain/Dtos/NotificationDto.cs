using System;
using System.Collections.Generic;

namespace Notifications.Domain.Dtos
{
    public class NotificationDto
    {
        public int? Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Message { get; set; }
        public bool? Viewed { get; set; }
        public ApplicationUserDto UserOwner { get; set; }
        public BusinessDto BusinessOwner { get; set; }
        public List<NotificationChannelRecordDto> Channels { get; set; }
        public List<RecipientDto> Recipients { get; set; }
        public NotificationDto() { }
        public NotificationDto(int? id, DateTime? createdDate, string title, string description, bool? viewed, ApplicationUserDto userOwner, BusinessDto businessOwner,  List<NotificationChannelRecordDto> channels, List<RecipientDto> recipients)
        {
            Id = id;
            CreatedDate = createdDate;
            Title = title;
            Description = description;
            Viewed = viewed;
            UserOwner = userOwner;
            BusinessOwner = businessOwner;
            Channels = channels;
            Recipients = recipients;
        }
    }
}
