using BrValidators;
using ExternalEntities.Domain.AggregatesModel.UserAggregate;
using ExternalEntities.Domain.Services;
using ExternalEntities.Infraestructure.Persistence;
using QuodProvider;
using System.Threading.Tasks;
using AuthenticationProvider.Abstractions;
using System;
using ExternalEntities.Domain.Dtos;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using QuodProvider.Dtos;
using ExternalEntities.Domain.AggregatesModel.CreditScoringAggregate;
using BacenProvider;
using BacenProvider.Dtos;
using ExternalEntities.Domain;

namespace ExternalEntities.Infraestructure.Services
{
    public abstract class CommomService : UserBaseService
    {
        protected readonly Configuration _config;
        protected readonly IQuodService _quodService;
        protected readonly IBacenService _bacenService;

        public CommomService(
            Configuration config,
            IIdentityService identityService,
            ExternalEntitiesContext context,
            IUserRepository userRepository,
            IQuodService externalEntityDataService,
            IBacenService bacenService,
            IAuthenticationService userService
        ) : base(context, userRepository, identityService, userService)
        {
            _config = config;
            _quodService = externalEntityDataService;
            _bacenService = bacenService;
        }

        protected UserDto getProfileData(ApplicationUser user)
        {
            var lastQuodData = user.GetCurrentQuodData();
            var name = lastQuodData?.Name?.Split(" ") ?? new string[] { "" };
            var firstName = name[0];
            name.ToList().RemoveAt(0);
            var lastName = string.Join(" ", name);

            return new UserDto(
                firstName,
                lastName,
                lastQuodData?.Email,
                lastQuodData?.MobilePhoneNumber,
                lastQuodData?.BirthDate,
                lastQuodData?.PublicExposed ?? false,
                default,
                lastQuodData?.Gender,
                lastQuodData?.CurrentAddress?.City,
                lastQuodData?.CurrentAddress?.PostalCode,
                lastQuodData?.CurrentAddress?.State,
                "Brasil",
                lastQuodData?.CurrentAddress?.Street,
                lastQuodData?.CurrentAddress?.Number,
                lastQuodData?.CurrentAddress?.Complement,
                lastQuodData?.CurrentAddress?.Neighborhood);
        }

        protected async Task<GetScoreDto> fullAnalysis(ApplicationUser user, string cpf)
        {
            if (!user.ShouldMakeFullAnalysis()) return convert(user);

            var useQuod = _config.UseQuod(cpf);

            var (quodData, quodSearchDone, scrResponse, bacenSearchDone)
                = await getExternalData(cpf, true, false, new(), new());

            try
            {
                user.AddQuodData(quodData, useQuod);
                await saveChangesAsyncConcurrency();
                (_, _, _, bacenSearchDone) = await getExternalData(cpf, false, !user.IsDenied, null, scrResponse);
                await addScrData(user, scrResponse, !user.IsDenied);
            }
            catch (Exception ex)
            {
                if (ex is DbUpdateException)
                {
                    user = await _userRepository.GetByCpf(cpf);
                    user.AddQuodData(quodData, useQuod);
                    await saveChangesAsyncConcurrency();

                    if (!bacenSearchDone)
                        await getExternalData(cpf, false, !user.IsDenied, null, scrResponse);

                    await addScrData(user, scrResponse, !user.IsDenied);
                }
                else throw;
            }

            return convert(user);
        }
        protected async Task<GetScoreDto> simulate(ApplicationUser user, string cpf)
        {
            if (!user.ShouldMakeSimulation()) return DtoConversor.ConvertForSimulation(user);

            var quodData = new ExternalQuodData();
            await getQuodScore(quodData, cpf);

            try
            {
                user.AddSimulation(quodData);
                await saveChangesAsyncConcurrency();
            }
            catch (Exception ex)
            {
                if (ex is DbUpdateException)
                {
                    user = await _userRepository.GetByCpf(cpf);
                    user.AddSimulation(quodData);
                    await saveChangesAsyncConcurrency();
                }
                else throw;
            }

            return DtoConversor.ConvertForSimulation(user);
        }
        protected async Task<(ExternalQuodData, bool, RbmScrResponse, bool)> getExternalData(
            string cpf,
            bool searchQuod,
            bool searchBacen,
            ExternalQuodData quodData,
            RbmScrResponse scrResponse)
        {
            bool quodSearchDone = false;
            bool bacenSearchDone = false;

            if (searchQuod)
            {
                await getQuodData(quodData, cpf);
                quodSearchDone = true;
            }

            if (searchBacen && !searchQuod)
            {
                await getBacenData(scrResponse, cpf);
                bacenSearchDone = true;
            }

            return (quodData, quodSearchDone, scrResponse, bacenSearchDone);
        }
        protected async Task getBacenData(RbmScrResponse scrResponse, string cpf)
        {
            if (_config.UseBacen(cpf))
            {
                var data = await _bacenService.GetFinancialData(cpf);
                if (scrResponse is null) scrResponse = new RbmScrResponse();
                scrResponse.Dados = data.Dados;
            }
        }
        protected async Task addScrData(ApplicationUser user, RbmScrResponse res, bool useBacen)
        {
            if (_config.UseBacen(user.Cpf) && useBacen)
            {
                var totalDelinquencyAmount = $"{res?.Dados?.Analise?.PrejuizoEmMeses?.Ate12 + res?.Dados?.Analise?.PrejuizoEmMeses?.AcimaDe12Ate48 + res?.Dados?.Analise?.PrejuizoEmMeses?.AcimaDe48}".TryParseDecimal();
                var totalFinancialOperations = res?.Dados?.QuantidadeDeOperacoes?.TryParseInt();
                var totalCurrentDebitAmount = $"{res?.Dados?.Analise?.Total?.AVencer}".TryParseDecimal();
                var totalCurrentDebitDelinquentAmount = $"{res?.Dados?.Analise?.Total?.Vencido}".TryParseDecimal();
                var totalCreditToBeAvailable =
                    res?.Dados?.Analise?.LimiteDeCreditoEmDias?.Ate360 +
                    res?.Dados?.Analise?.LimiteDeCreditoEmDias?.AcimaDe360 +
                    res?.Dados?.Analise?.CreditoALiberarEmDias?.Ate360 +
                    res?.Dados?.Analise?.CreditoALiberarEmDias?.AcimaDe360;
                var totalOverdrawnAmount =
                    res?.Dados?.ListaDeResumoDasOperacoes
                    ?.Where(lop => lop?.Modalidade == "0213" || lop?.Modalidade == "0201")
                    ?.Sum(lop => lop?.ListaDeVencimentos?.Where(lv => lv?.ValorVencimento?.TryParseDecimal() > 0)
                    ?.Sum(lv => lv?.ValorVencimento?.TryParseDecimal()));

                user.AddFinancialAnalysisData(totalDelinquencyAmount, totalCurrentDebitAmount, totalCurrentDebitDelinquentAmount, totalOverdrawnAmount, $"{totalCreditToBeAvailable}".TryParseDecimal(), totalFinancialOperations, useBacen);
                await saveChangesAsyncConcurrency();
            }
        }
        protected async Task getQuodData(ExternalQuodData quodData, string cpf)
        {
            Record quodRes = new Record();
            PresumedIncomeOutput presumedRecord = new PresumedIncomeOutput();
            if (_config.UseQuod(cpf) || _config.IsTestCpf(cpf))
            {
                var data = await _quodService.SearchUser(_config.IsTestCpf(cpf) ? "84509848072" : cpf);
                quodRes = data.QuodResponse;
                presumedRecord = data.QuodPresumedIncomeResponse?.PresumedIncomeResponseEx?.Response?.Records?.PresumedIncomeOutput?.FirstOrDefault();
            }
            else
                quodRes.QuodScore = new() { Score = 780 };

            convert(quodData, quodRes, presumedRecord);
        }
        protected async Task getQuodScore(ExternalQuodData quodData, string cpf)
        {
            Record quodRes = new Record();
            if (_config.UseQuod(cpf) || _config.IsTestCpf(cpf))
            {
                var data = await _quodService.GetScore(_config.IsTestCpf(cpf) ? "84509848072" : cpf);
                quodRes.QuodScore = data.QuodResponse.QuodScore;
            }
            else
                quodRes.QuodScore = new() { Score = 780 };

            convert(quodData, quodRes, null);
        }
        protected GetScoreDto convert(ApplicationUser user)
        {
            if (!_config.UseBacen(user.Cpf) && !_config.UseQuod(user.Cpf))
                return DtoConversor.ConvertForSimulation(user);

            return DtoConversor.Convert(user);
        }
        protected ExternalQuodData convert(ExternalQuodData externalData, Record quodRecord, PresumedIncomeOutput presumedRecord)
        {
            if (quodRecord?.QuodScore is not null)
                externalData.Score = new ExternalQuodScore(
                    quodRecord.QuodScore.Score,
                    quodRecord.QuodScore.Segment,
                    quodRecord.QuodScore.PaymentCommitmentScore,
                    quodRecord.QuodScore.ProfileScore);

            if (quodRecord?.Negative is not null)
                externalData.Negative = new ExternalQuodNegative(NumberHelper.FromDecimalString(
                    !string.IsNullOrEmpty(quodRecord.Negative.PendenciesControlCred) ? quodRecord.Negative.PendenciesControlCred : "0"));

            if (quodRecord?.Protests?.Protest is not null)
                externalData.Protests = quodRecord.Protests.Protest
                .Where(e =>
                    !string.IsNullOrEmpty(e.totalProtestos) ||
                    !string.IsNullOrEmpty(e.valor_protestados_total) ||
                    !string.IsNullOrEmpty(e.transactionId) ||
                    !string.IsNullOrEmpty(e.situacao))
                .Select(e => new ExternalQuodProtest(
                    e.situacao,
                    e.valor_protestados_total,
                    e.totalProtestos))
                ?.ToList();

            if (quodRecord?.BestInfo?.DOB is not null)
                externalData.BirthDate = new DateTime(Convert.ToInt32(quodRecord?.BestInfo?.DOB.Year), Convert.ToInt32(quodRecord?.BestInfo?.DOB.Month), Convert.ToInt32(quodRecord?.BestInfo?.DOB.Day));

            if (quodRecord?.Pep is not null)
                externalData.PublicExposed = quodRecord?.Pep;

            if (quodRecord?.BestInfo?.Gender is not null)
                externalData.Gender = quodRecord.BestInfo.Gender;

            if (quodRecord?.BestInfo?.PersonName?.Name is not null)
                externalData.Name = quodRecord.BestInfo.PersonName.Name.Full;

            if (quodRecord?.BestInfo?.Email is not null)
                externalData.Email = quodRecord.BestInfo.Email.Email;

            if (quodRecord?.BestInfo?.EmailHistory?.Email is not null)
                externalData.Emails = quodRecord.BestInfo.EmailHistory.Email
                .Select(e => new ExternalQuodEmail(
                    e.Email,
                    new DateTime(Convert.ToInt32(e.DateLastSeen?.Year), Convert.ToInt32(e.DateLastSeen?.Month), Convert.ToInt32(e.DateLastSeen?.Day))))
                .ToList();

            if (quodRecord?.BestInfo?.PhoneNumber is not null)
                externalData.PhoneNumber = quodRecord.BestInfo.PhoneNumber.PhoneNumber;

            if (quodRecord?.BestInfo?.PhoneNumberHistory?.PhoneNumber is not null)
                externalData.PhoneNumbers = quodRecord.BestInfo.PhoneNumberHistory.PhoneNumber
                .Select(e => new ExternalQuodPhoneNumber(
                    e.PhoneNumber,
                    new DateTime(Convert.ToInt32(e.DateLastSeen?.Year), Convert.ToInt32(e.DateLastSeen?.Month), Convert.ToInt32(e.DateLastSeen?.Day)))).ToList();

            if (quodRecord?.BestInfo?.MobilePhoneNumber is not null)
                externalData.MobilePhoneNumber = quodRecord.BestInfo.MobilePhoneNumber.PhoneNumber;

            if (quodRecord?.BestInfo?.MobilePhoneNumberHistory?.MobilePhoneNumber is not null)
                externalData.MobilePhoneNumbers = quodRecord.BestInfo.MobilePhoneNumberHistory.MobilePhoneNumber
                .Select(e => new ExternalQuodPhoneNumber(
                    e.PhoneNumber,
                    new DateTime(Convert.ToInt32(e.DateLastSeen?.Year), Convert.ToInt32(e.DateLastSeen?.Month), Convert.ToInt32(e.DateLastSeen?.Day))))
                .ToList();

            if (quodRecord?.BestInfo?.Address is not null)
                externalData.CurrentAddress = new ExternalQuodAddress(
                    quodRecord.BestInfo.Address.Street,
                    quodRecord.BestInfo.Address.Number,
                    quodRecord.BestInfo.Address.Complement,
                    quodRecord.BestInfo.Address.Neighborhood,
                    quodRecord.BestInfo.Address.City,
                    quodRecord.BestInfo.Address.State,
                    quodRecord.BestInfo.Address.PostalCode);

            if (quodRecord?.BestInfo?.AddressHistory?.Address is not null)
                externalData.Addresses = quodRecord.BestInfo.AddressHistory.Address
                .Select(e => new ExternalQuodAddress(
                    e.Street,
                    e.Number,
                    e.Complement,
                    e.Neighborhood,
                    e.City,
                    e.State,
                    e.PostalCode))
                .ToList();

            return externalData;
        }
    }
}
