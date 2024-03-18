using System.Threading.Tasks;

namespace Notifications.Domain.Services
{
    public interface IBusinessService
    {
        Task Add(int id, string name, string email, string cellphone, int[] userIds);
        Task Update(int id, string name, string email, string cellphone, int[] userIds);
    }
}
