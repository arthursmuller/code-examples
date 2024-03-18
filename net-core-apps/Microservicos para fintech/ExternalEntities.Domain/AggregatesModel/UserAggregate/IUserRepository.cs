using ExternalEntities.Domain.Abstractions;
using System.Threading.Tasks;

namespace ExternalEntities.Domain.AggregatesModel.UserAggregate
{
    public interface IUserRepository : IRepositoryBase<ApplicationUser> 
    {
        Task<ApplicationUser> Get(int id);
        Task<ApplicationUser> Delete(int id);
        Task<ApplicationUser> GetByCpf(string cpf);
        Task<int> AddIgnore(ApplicationUser user);
    }
}
