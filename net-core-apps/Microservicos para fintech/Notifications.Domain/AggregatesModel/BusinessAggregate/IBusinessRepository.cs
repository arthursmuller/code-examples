using Notifications.Domain.Abstractions;
using System.Threading.Tasks;

namespace Notifications.Domain.AggregatesModel.BusinessAggregate
{
    public interface IBusinessRepository : IRepositoryBase<Business> 
    {
        Task<Business> Get(int businessId);
        Task<Business> Delete(int businessId);
    }
}
