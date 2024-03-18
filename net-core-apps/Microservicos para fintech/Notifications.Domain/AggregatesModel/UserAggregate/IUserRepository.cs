using Notifications.Domain.Abstractions;
using System.Threading.Tasks;

namespace Notifications.Domain.AggregatesModel.UserAggregate
{
    public interface IUserRepository : IRepositoryBase<ApplicationUser> 
    {
        Task<ApplicationUser> Get(int businessId);
        Task<ApplicationUser> Delete(int businessId);
    }
}
