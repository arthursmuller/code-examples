using Microsoft.EntityFrameworkCore;
using Signature.Domain.AggregatesModel.SignatureAggregate;
using Signature.Domain.Services;
using Signature.Infraestructure.Persistence;
using System;
using System.Threading.Tasks;

namespace Signature.Infraestructure.Services
{
    public abstract class BaseService
    {
        protected readonly IIdentityService _identityService;
        protected readonly ISignatureRepository _signatureRepository;
        protected readonly SignatureContext _signatureContext;

        public BaseService(SignatureContext storageContext, ISignatureRepository signatureRepository, IIdentityService identityService)
        {
            _signatureContext = storageContext ?? throw new ArgumentNullException(nameof(storageContext));
            _signatureRepository = signatureRepository ?? throw new ArgumentNullException(nameof(signatureRepository));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        }

        protected async Task<int> saveChangesAsyncConcurrency()
        {
            var saved = false;
            while (!saved)
            {
                try
                {
                    await _signatureRepository.UnitOfWork.SaveChangesAsync();
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
            await _signatureContext.Set<T>().AddAsync(entity);
            await saveChangesAsync();
        }
        private async Task saveChangesAsync() => await _signatureRepository.UnitOfWork.SaveChangesAsync();
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
