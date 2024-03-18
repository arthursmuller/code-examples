using Microsoft.EntityFrameworkCore;
using ExternalEntities.Domain.AggregatesModel.UserAggregate;
using ExternalEntities.Domain.Services;
using ExternalEntities.Infraestructure.Persistence;
using System;
using System.Threading.Tasks;
using AuthenticationProvider.Abstractions;
using Newtonsoft.Json;

namespace ExternalEntities.Infraestructure.Services
{
    public abstract class UserBaseService
    {
        protected readonly IIdentityService _identityService;
        protected readonly IUserRepository _userRepository;
        protected readonly ExternalEntitiesContext _context;
        private readonly IAuthenticationService _userService;

        public UserBaseService(ExternalEntitiesContext notificationsContext, IUserRepository userRepository, IIdentityService identityService, IAuthenticationService userService)
        {
            _context = notificationsContext ?? throw new ArgumentNullException(nameof(notificationsContext));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        protected async Task<int> saveChangesAsyncConcurrency()
        {
            var saved = false;
            while (!saved)
            {
                try
                {
                    await _userRepository.UnitOfWork.SaveChangesAsync();
                    saved = true;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    handleConcurrencyException(ex);
                }
            }

            return 1;
        }
        protected async Task addAndSaveAsync<T>(T entity) where T : class
        {
            await _context.Set<T>().AddAsync(entity);
            await saveChangesAsync();
        }
        private async Task saveChangesAsync() => await _userRepository.UnitOfWork.SaveChangesAsync();
        private void handleConcurrencyException(DbUpdateConcurrencyException ex)
        {
            foreach (var entry in ex.Entries)
            {
                var proposedValues = entry.CurrentValues;
                var databaseValues = entry.GetDatabaseValues();

                foreach (var property in proposedValues.Properties)
                {
                    var proposedValue = proposedValues[property];
                    var databaseValue = databaseValues[property];
                }

                entry.OriginalValues.SetValues(databaseValues);
            }
        }

        protected async Task<ApplicationUser> makeSureUserExists(ApplicationUser existing, string cpf = null, int? id = null)
        {
            if (existing is null && (!string.IsNullOrEmpty(cpf) || id is not null))
            {
                var userCpf = cpf;
                int? userId = id;
                if (!string.IsNullOrEmpty(cpf))
                {
                    userId = await _userService.GetUserIdByCpf(userCpf);
                } 
                else if (id is not null)
                {
                    var userData = await _userService.GetUserDataById((int)id);
                    userId = userData.Id;
                    userCpf = userData.Cpf;
                }

                if (userId is not null)
                {
                    try
                    {
                        existing = new ApplicationUser((int)userId, userCpf);
                        await addAndSaveAsync(existing);
                    }
                    catch(DbUpdateException ex)
                    {
                        var existingUser = await _userRepository.GetByCpf(userCpf);
                        existing = existingUser;
                    }
                }
            }

            return existing;
        }
    }
}
