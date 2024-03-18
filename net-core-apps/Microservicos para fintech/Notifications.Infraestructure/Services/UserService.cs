using Notifications.Domain.AggregatesModel.UserAggregate;
using Notifications.Domain.Services;
using Notifications.Infraestructure.Persistence;
using System.Threading.Tasks;

namespace Notifications.Infraestructure.Services
{
    public class UserService : UserBaseService, IUserService
    {
        public UserService(NotificationsContext context, IIdentityService identityService, IUserRepository businessRespository)
            : base(context, businessRespository, identityService) { }

        public async Task Add(int id, string name, string email, string cellphone)
        {
            var existing = await _userRepository.Get(id);

            if (existing is null)
                await addAndSaveAsync(new ApplicationUser(id, name, email, cellphone));
        }

        public async Task Update(int id, string name = null, string email = null, string cellphone = null)
        {
            var existing = await _userRepository.Get(id);
            if(name is not null)
                existing.Name = name;
            if(email is not null)
                existing.Email = email;
            if(cellphone is not null)
                existing.Cellphone = cellphone;

            await saveChangesAsyncConcurrency();
        }
    }
}
