using AuthenticationProvider.Abstractions;
using BacenProvider;
using ExternalEntities.Domain.AggregatesModel.BusinessAggregate;
using ExternalEntities.Domain.AggregatesModel.CreditScoringAggregate;
using ExternalEntities.Domain.AggregatesModel.UserAggregate;
using ExternalEntities.Domain.Dtos;
using ExternalEntities.Domain.Services;
using ExternalEntities.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using QuodProvider;
using System.Linq;
using System.Threading.Tasks;

namespace ExternalEntities.Infraestructure.Services
{
    public class BusinessService : CommomService, IBusinessService
    {
        private readonly IBusinessRepository _businessRepository;
        public BusinessService(
            IBusinessRepository businessRepository,
            Configuration config,
            IIdentityService identityService,
            ExternalEntitiesContext context,
            IUserRepository userRepository,
            IQuodService externalEntityDataService,
            IBacenService bacenService,
            IAuthenticationService userService
        ) : base(config, identityService, context, userRepository, externalEntityDataService, bacenService, userService)
        {
            _businessRepository = businessRepository;
        }

        public async Task Add(int id, string cnpj, int[] userIds)
        {
            var existing = await _businessRepository.Get(id);

            if (existing is null)
                await addAndSaveAsync(new Business(id, cnpj, userIds));
        }
        public async Task Update(int id, string cnpj, int[] userIds)
        {
            var existing = await _businessRepository.Get(id);
            existing.Cnpj = cnpj;

            if (userIds is not null)
                existing.UpdateOwners(userIds);

            await saveChangesAsyncConcurrency();
        }
        public async Task<AnalysisResDto> PayiedAnalysis(PayiedAnalysisDto req)
        {
            var user = await _userRepository.Get(req.UserId);
            user = await makeSureUserExists(user, id: req.UserId);

            if (req.Complete)
                await fullAnalysis(user, user.Cpf);
            else if (req.Score)
                await simulate(user, user.Cpf);

            await addPayiedAnalysis(req.BusinessId, user.Cpf, req.Complete);

            var res = new AnalysisResDto(
                DtoConversor.Convert(user),
                req.Complete ? DtoConversor.FinancialDto(user) : null,
                req.Basic ? getProfileData(user) : null,
                req.Complete ? DtoConversor.AnalysisDashboardDto(user) : null
            );

            return res;
        }
        private async Task addPayiedAnalysis(int businessId, string userCpf, bool newAnalysIsComplete)
        {
            var existingAnalysis = await _context.AnalysisRequests
                .OrderByDescending(e => e.CreatedDate)
                .Where(e => e.Business.Id == businessId && e.Cpf == userCpf)
                .FirstOrDefaultAsync();

            if(existingAnalysis?.ShouldCharge() ?? true)
            {
                var business = await _businessRepository.Get(businessId);
                var analysis = new AnalysisRequest(userCpf, newAnalysIsComplete, businessId);
                analysis.Cost = newAnalysIsComplete ? AnalysisRequestPrice.Complete.Cost : AnalysisRequestPrice.Score.Cost;
                business.AddPayiedScore(analysis);

                await saveChangesAsyncConcurrency();
            }
        }
    }
}
