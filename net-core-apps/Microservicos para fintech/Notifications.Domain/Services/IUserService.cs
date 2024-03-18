using System.Threading.Tasks;

namespace Notifications.Domain.Services
{
    public interface IUserService
    {
        Task Add(int id, string name, string email, string cellphone);
        Task Update(int id, string name = null, string email = null, string cellphone = null);
    }
}
