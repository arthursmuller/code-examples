using Notifications.Domain.AggregatesModel.BusinessAggregate;
using Notifications.Domain.AggregatesModel.NotificationAggregate;
using Notifications.Domain.AggregatesModel.UserAggregate;
using Notifications.Domain.Dtos;
using Notifications.Domain.Services;
using Notifications.Infraestructure.Persistence;
using Notifications.Infraestructure.Providers.Email;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notifications.Infraestructure.Services
{
    public class NotificationService : BaseService, INotificationService
    {
        private readonly IEmailService _emailService;
        private readonly IBusinessRepository _businessRespository;
        private readonly IUserRepository _userRespository;
        public NotificationService(
            NotificationsContext context, 
            INotificationRepository notificationRepository,
            IIdentityService identityService, 
            IEmailService emailService, 
            IBusinessRepository businessRespository, 
            IUserRepository userRespository)
            : base(context, notificationRepository, identityService)
        {
            _emailService = emailService;
            _businessRespository = businessRespository;
            _userRespository = userRespository;
        }

        public async Task<NotificationDto> Add(int? businessId = null, int? userId = null, string message = null, string description = null, string title = null, List<int> channelIds = null, List<RecipientDto> recipients = null, IDictionary<string, string> replacements = null, string templateFileName = null, string template = null)
        {
            NotificationBase newNotification = null;
            var isBusinessNotification = businessId is not null;

            if (isBusinessNotification)
            {
                newNotification = businessId is not null && businessId > 0 ? new BusinessNotification((int)businessId) : new BusinessNotification();
                var business = await _businessRespository.Get(businessId ?? 0);
                
                if(business is not null)
                    newNotification.AddRecipient(business?.Name, business?.Email, business?.Cellphone);

                recipients = recipients?.Where(e => e.Email != business?.Email)?.ToList();
            }
            else
            {
                newNotification = userId is not null && userId > 0 ? new UserNotification((int) userId) : new UserNotification();
                var user = await _userRespository.Get(userId ?? 0);
                
                if(user is not null)
                    newNotification.AddRecipient(user?.Name, user?.Email, user?.Cellphone);

                recipients = recipients?.Where(e => e?.Email != user?.Email)?.ToList();
            }

            newNotification.Title = title;
            newNotification.Description = description;
            newNotification.Message = message;

            if (recipients is not null)
                foreach (var recipient in recipients)
                    newNotification.AddRecipient(recipient.Name, recipient.Email, recipient.Cellphone);

            if (channelIds is not null)
                foreach (var id in channelIds)
                    newNotification.AddChannel(id);

            await handleChannels(newNotification, templateFileName, template, replacements);

            return DtoConversor.convertToNotificationDto(newNotification);
        }
        private async Task handleChannels(NotificationBase notification, string templateFileName, string template, IDictionary<string, string> replacements)
        {
            var shouldSendEmail = notification?.GetChannels()?.Any(e => e.Id == NotificationChannel.Email.Id) ?? false;
            var shouldSendSms = notification?.GetChannels()?.Any(e => e.Id == NotificationChannel.Sms.Id) ?? false;
            var shouldSaveInDb = notification?.GetChannels()?.Any(e => e.Id == NotificationChannel.Platform.Id) ?? false;

            if (shouldSendEmail)
                await sendEmail(notification, templateFileName, template, replacements);

            if (shouldSaveInDb)
                await addAndSaveAsync(notification);
        }

        public async Task<List<NotificationDto>> ListMyNotifications()
        {
            var notifications = await _notificationsRepository.ListUserNotifications(_identityService.UserId);
            return DtoConversor.convertToNotificationDto(notifications);
        }
        public async Task<List<NotificationDto>> ListBusinessNotifications(int businessId)
        {
            var notifications = await _notificationsRepository.ListBusinessNotifications(businessId, _identityService.UserId);
            return DtoConversor.convertToNotificationDto(notifications);
        }
        private async Task<bool> sendEmail(NotificationBase notification, string templateFileName, string template, IDictionary<string, string> replacements) 
            => await _emailService.Send(notification.Title, DtoConversor.convertToRecipientlDto(notification.Recipients), notification.Message, templateFileName, template, replacements);
    }
}
