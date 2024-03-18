using Microsoft.EntityFrameworkCore;
using Notifications.Domain.Abstractions;
using Notifications.Domain.AggregatesModel.NotificationAggregate;
using Notifications.Domain.AggregatesModel.UserAggregate;
using Notifications.Infraestructure.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notifications.Infraestructure.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly NotificationsContext _context;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }
        public NotificationRepository(NotificationsContext context) => (_context) = (context);

        public async Task<List<UserNotification>> ListUserNotifications(int userId) 
            =>  await _context
                    .UserNotifications
                        .Include(e => e.UserOwner)
                        .Where(u => u.UserOwner.Id == userId).OrderByDescending(e => e.CreatedDate)
                        .ToListAsync();
        public async Task<List<BusinessNotification>> ListBusinessNotifications(int businessId, int userId) 
            =>  await _context
                    .BusinessNotifications
                        .Include(e => e.BusinessOwner)
                            .ThenInclude(e => e.Owners)
                                .ThenInclude(e => e.User)
                        .Where(u => u.BusinessOwner.Id == businessId && u.BusinessOwner.Owners.Any(e => e.User.Id == userId))
                        .OrderByDescending(e => e.CreatedDate)
                        .ToListAsync();
        public Task<ApplicationUser> Delete(int businessId)
        {
            throw new System.NotImplementedException();
        }
    }
}
