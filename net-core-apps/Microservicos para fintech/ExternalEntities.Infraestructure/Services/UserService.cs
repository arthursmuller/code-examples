using BrValidators;
using ExternalEntities.Domain.AggregatesModel.UserAggregate;
using ExternalEntities.Domain.Services;
using ExternalEntities.Infraestructure.Persistence;
using QuodProvider;
using System.Threading.Tasks;
using AuthenticationProvider.Abstractions;
using ExternalEntities.Domain.Dtos;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Notifications.Domain.Exceptions;
using BacenProvider;

namespace ExternalEntities.Infraestructure.Services
{
    public class UserService : CommomService, IUserService
    {
        public UserService(
            Configuration config,
            IIdentityService identityService,
            ExternalEntitiesContext context,
            IUserRepository userRepository,
            IQuodService externalEntityDataService,
            IBacenService bacenService,
            IAuthenticationService userService
        ) : base(config, identityService, context, userRepository, externalEntityDataService, bacenService, userService) { }

        public async Task Add(int id, string cpf)
        {
            try
            {
                await addAndSaveAsync(new ApplicationUser(id, cpf));
            } catch (DbUpdateException ex)
            {
                var existing = await _userRepository.Get(id);
                if (existing is null) throw new ExternalEntitiesException("User does not exist");
            }
        }
        public async Task<UserDto> Get(string cpf)
        {
            var score = await GetScore(cpf);
            var user = await _userRepository.Get(score.Id);
            return getProfileData(user);
        }
        public async Task<GetScoreDto> GetScore(string cpf)
        {
            cpf = CPFValidator.Trim(cpf);
            var user = await _userRepository.GetByCpf(cpf);
            user = await makeSureUserExists(user, cpf);
            return await fullAnalysis(user, cpf);
        }
        public async Task<GetScoreDto> GetScore(int id)
        {
            var user = await _userRepository.Get(id);
            user = await makeSureUserExists(user, id: id);
            return await fullAnalysis(user, user.Cpf);
        }
        public async Task<GetScoreDto> SimulateScore(string cpf)
        {
            cpf = CPFValidator.Trim(cpf);
            var user = await _userRepository.GetByCpf(cpf);
            user = await makeSureUserExists(user, cpf);
            return await simulate(user, user.Cpf);
        }
        public async Task<GetScoreDto> SimulateScore(int id)
        {
            var user = await _userRepository.Get(id);
            user = await makeSureUserExists(user, id: id);
            return await simulate(user, user.Cpf);
        }
        public async Task Update(int id, string cpf)
        {
            var existing = await _userRepository.Get(id);
            existing.Cpf = cpf;
            await saveChangesAsyncConcurrency();
        }
    }
}
