using Notifications.Domain.AggregatesModel.BusinessAggregate;
using Notifications.Domain.Services;
using Notifications.Infraestructure.Persistence;
using System.Threading.Tasks;

namespace Notifications.Infraestructure.Services
{
    public class BusinessService : BusinessBaseService, IBusinessService
    {
        public BusinessService(NotificationsContext context, IIdentityService identityService, IBusinessRepository userRespository)
            : base(context, userRespository, identityService) { }

        public async Task Add(int id, string name, string email, string cellphone, int[] userIds)
        {
            var existing = await _businessRepository.Get(id);

            if (existing is null)
                await addAndSaveAsync(new Business(id, name, email, cellphone, userIds));
        }

        public async Task Update(int id, string name, string email, string cellphone, int[] userIds)
        {
            var existing = await _businessRepository.Get(id);
            existing.Name = name;
            existing.Email = email;
            existing.Cellphone = cellphone;

            if (userIds is not null)
                existing.UpdateOwners(userIds);

            await saveChangesAsyncConcurrency();
        }
    }
}
