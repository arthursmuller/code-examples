using Notifications.Domain.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notifications.Domain.AggregatesModel.NotificationAggregate
{
    public interface INotificationRepository : IRepositoryBase<NotificationBase>
    {
        Task<List<UserNotification>> ListUserNotifications(int userId);
        Task<List<BusinessNotification>> ListBusinessNotifications(int businessId, int userId);
    } 
}
