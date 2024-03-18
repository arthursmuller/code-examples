using Notifications.Domain.AggregatesModel.NotificationAggregate;
using Notifications.Domain.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notifications.Domain.Services
{
    public interface INotificationService
    {
        Task<NotificationDto> Add(int? businessId = null, int? userId = null, string message = null, string description = null, string title = null, List<int> channelIds = null, List<RecipientDto> recipients = null, IDictionary<string, string> replacements = null, string templateFileName = null, string template = null);
    }
}
