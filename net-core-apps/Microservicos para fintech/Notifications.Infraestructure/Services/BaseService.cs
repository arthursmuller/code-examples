using Microsoft.EntityFrameworkCore;
using Notifications.Domain.AggregatesModel.NotificationAggregate;
using Notifications.Domain.Services;
using Notifications.Infraestructure.Persistence;
using System;
using System.Threading.Tasks;

namespace Notifications.Infraestructure.Services
{
    public abstract class BaseService
    {
        protected readonly IIdentityService _identityService;
        protected readonly INotificationRepository _notificationsRepository;
        protected readonly NotificationsContext _notificationsContext;

        public BaseService(NotificationsContext notificationsContext, INotificationRepository notificationsRepository, IIdentityService identityService)
        {
            _notificationsContext = notificationsContext ?? throw new ArgumentNullException(nameof(notificationsContext));
            _notificationsRepository = notificationsRepository ?? throw new ArgumentNullException(nameof(notificationsRepository));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        }

        protected async Task<int> saveChangesAsyncConcurrency()
        {
            var saved = false;
            while (!saved)
            {
                try
                {
                    await _notificationsRepository.UnitOfWork.SaveChangesAsync();
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
            await _notificationsContext.Set<T>().AddAsync(entity);
            await saveChangesAsync();
        }
        private async Task saveChangesAsync() => await _notificationsRepository.UnitOfWork.SaveChangesAsync();
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

                    // TODO: decide which value should be written to database
                    // proposedValues[property] = <value to be saved>;
                }

                // Refresh original values to bypass next concurrency check
                entry.OriginalValues.SetValues(databaseValues);
            }
        }
    }
}
