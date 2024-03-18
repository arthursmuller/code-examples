using Notifications.Domain.AggregatesModel.BusinessAggregate;
using Notifications.Domain.AggregatesModel.NotificationAggregate;
using Notifications.Domain.AggregatesModel.UserAggregate;
using System.Collections.Generic;
using System.Linq;

namespace Notifications.Domain.Dtos
{
    public static class DtoConversor
    {
        public static List<NotificationDto> convertToNotificationDto(List<UserNotification> notifications) => notifications?.Select(notification => convertToNotificationDto(notification))?.ToList();
        public static List<NotificationDto> convertToNotificationDto(List<BusinessNotification> notifications) => notifications?.Select(notification => convertToNotificationDto(notification))?.ToList();
        public static NotificationDto convertToNotificationDto(NotificationBase notification)
        {
            BusinessNotification businessNotification = null;
            UserNotification userNotification = null;

            try
            {
                businessNotification = (BusinessNotification) notification;
            }
            catch 
            {
                userNotification = (UserNotification) notification;
            }

            return new NotificationDto(
                notification?.Id,
                notification?.CreatedDate,
                notification?.Title,
                notification?.Description,
                notification?.Viewed,
                ConvertToUserDto(userNotification?.UserOwner),
                ConvertToBusinessDto(businessNotification?.BusinessOwner),
                convertToRecipientlDto(notification?.Channels?.ToList()),
                convertToRecipientlDto(notification?.Recipients?.ToList())
            );
        }

        public static List<RecipientDto> convertToRecipientlDto(IReadOnlyCollection<Recipient> recipients)
        {
            var list = new List<RecipientDto>();
            if (recipients is not null)
                foreach (var recipient in recipients)
                    list.Add(convertToRecipientDto(recipient));

            return list;
        }
        public static List<RecipientDto> convertToRecipientlDto(List<Recipient> recipients)
        {
            var list = new List<RecipientDto>();
            if (recipients is not null)
                foreach (var recipient in recipients)
                    list.Add(convertToRecipientDto(recipient));

            return list;
        }

        public static List<NotificationChannelRecordDto> convertToRecipientlDto(List<NotificationChannelRecord> records)
        {
            var list = new List<NotificationChannelRecordDto>();
            if (records is not null)
                foreach (var record in records)
                    list.Add(convertToNotificationChannelRecordDto(record));

            return list;
        }

        public static RecipientDto convertToRecipientDto(Recipient recipient) =>
            new RecipientDto(
                recipient?.Name,
                recipient?.Email,
                recipient?.Cellphone);
        public static NotificationChannelRecordDto convertToNotificationChannelRecordDto(NotificationChannelRecord notificationChannelRecord) =>
            new NotificationChannelRecordDto(convertToNotificationChannelDto(notificationChannelRecord?.Channel));
        public static NotificationChannelDto convertToNotificationChannelDto(NotificationChannel channel) =>
            new NotificationChannelDto(
                channel?.Id,
                channel?.Name);

        public static ApplicationUserDto ConvertToUserDto(ApplicationUser user) =>
            new ApplicationUserDto(
                user?.Id,
                user?.Email,
                user?.Cellphone);

        public static BusinessDto ConvertToBusinessDto(Business business) =>
            new BusinessDto(
                business?.Id,
                business?.Email,
                business?.Cellphone);

    }
}
