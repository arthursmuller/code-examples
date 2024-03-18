using Microsoft.EntityFrameworkCore;
using Notifications.Domain.Abstractions;
using Notifications.Domain.AggregatesModel.BusinessAggregate;
using Notifications.Infraestructure.Persistence;
using System.Threading.Tasks;

namespace Notifications.Infraestructure.Repositories
{
    public class BusinessRepository : IBusinessRepository
    {
        private readonly NotificationsContext _context;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }
        public BusinessRepository(NotificationsContext context) => (_context) = (context);

        public async Task<Business> Get(int businessId) =>  await _context.Businesses.FirstOrDefaultAsync(u => u.Id == businessId);

        public Task<Business> Delete(int businessId)
        {
            throw new System.NotImplementedException();
        }
    }
}
