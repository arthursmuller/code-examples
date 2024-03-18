using ExternalEntities.Domain.Abstractions;
using System.Threading.Tasks;

namespace ExternalEntities.Domain.AggregatesModel.BusinessAggregate
{
    public interface IBusinessRepository : IRepositoryBase<Business> 
    {
        Task<Business> Get(int businessId);
        Task<Business> Delete(int businessId);
    }
}
