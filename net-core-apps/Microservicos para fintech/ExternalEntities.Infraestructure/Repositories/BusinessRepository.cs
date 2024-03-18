using Microsoft.EntityFrameworkCore;
using ExternalEntities.Domain.Abstractions;
using ExternalEntities.Domain.AggregatesModel.BusinessAggregate;
using ExternalEntities.Infraestructure.Persistence;
using System.Threading.Tasks;

namespace ExternalEntities.Infraestructure.Repositories
{
    public class BusinessRepository : IBusinessRepository
    {
        private readonly ExternalEntitiesContext _context;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }
        public BusinessRepository(ExternalEntitiesContext context) => (_context) = (context);

        public async Task<Business> Get(int businessId) =>  await _context.Businesses.Include(u => u.Scores).FirstOrDefaultAsync(u => u.Id == businessId);

        public Task<Business> Delete(int businessId)
        {
            throw new System.NotImplementedException();
        }
    }
}
