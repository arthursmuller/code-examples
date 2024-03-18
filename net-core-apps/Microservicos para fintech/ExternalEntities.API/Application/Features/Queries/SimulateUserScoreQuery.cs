using ExternalEntities.Domain.AggregatesModel.UserAggregate;
using ExternalEntities.Domain.Dtos;
using ExternalEntities.Domain.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ExternalEntities.API.Application.Features.Queries
{
    public class SimulateUserScoreQuery : IRequest<GetScoreDto>
    {
        public string Cpf { get; }

        public SimulateUserScoreQuery(string cpf)
        {
            Cpf = cpf;
        }
    }

    public class SimulateUserScoreHandler : IRequestHandler<SimulateUserScoreQuery, GetScoreDto>
    {
        private readonly ILogger<SimulateUserScoreHandler> _logger;
        private readonly IUserService _service;

        public SimulateUserScoreHandler(
            IUserService service,
            ILogger<SimulateUserScoreHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<GetScoreDto> Handle(SimulateUserScoreQuery request, CancellationToken cancellation)
        {
            try
            {
                return await _service.SimulateScore(request.Cpf);
            }
            catch (Exception ex)
            {
                if (!(ex is DbUpdateException))
                {
                    _logger.LogInformation(ex, "SimulateUserScoreQuery fail,error:{ex.Message},stackTrace:{ex.StackTrace},cpf:{cpf} :", ex.Message, ex.StackTrace, request.Cpf);
                    _logger.LogError(-1, ex, "SimulateUserScoreQuery Process fail");
                    throw;
                }
                
                return await _service.GetScore(request.Cpf);
            }
        }
    }
}
