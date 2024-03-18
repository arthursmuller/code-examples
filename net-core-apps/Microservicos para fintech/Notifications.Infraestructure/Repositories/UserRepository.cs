using Microsoft.EntityFrameworkCore;
using Notifications.Domain.Abstractions;
using Notifications.Domain.AggregatesModel.UserAggregate;
using Notifications.Infraestructure.Persistence;
using System.Threading.Tasks;

namespace Notifications.Infraestructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly NotificationsContext _context;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }
        public UserRepository(NotificationsContext context) => (_context) = (context);

        public async Task<ApplicationUser> Get(int userId) =>  await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

        public Task<ApplicationUser> Delete(int businessId)
        {
            throw new System.NotImplementedException();
        }
    }
}
