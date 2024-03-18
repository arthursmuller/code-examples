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
    public class SimulateUserScoreByIdQuery : IRequest<GetScoreDto>
    {
        public int Id { get; }

        public SimulateUserScoreByIdQuery(int id)
        {
            Id = id;
        }
    }

    public class SimulateUserScoreByIdHandler : IRequestHandler<SimulateUserScoreByIdQuery, GetScoreDto>
    {
        private readonly ILogger<SimulateUserScoreByIdHandler> _logger;
        private readonly IUserService _service;

        public SimulateUserScoreByIdHandler(
            IUserService service,
            ILogger<SimulateUserScoreByIdHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<GetScoreDto> Handle(SimulateUserScoreByIdQuery request, CancellationToken cancellation)
        {
            try
            {
                return await _service.SimulateScore(request.Id);
            }
            catch (Exception ex)
            {
                if (!(ex is DbUpdateException))
                {
                    _logger.LogInformation(ex, "SimulateUserScoreByIdQuery fail,error:{0},stackTrace:{1},id:{2} :", ex.Message, ex.StackTrace, request.Id);
                    _logger.LogError(-1, ex, "SimulateUserScoreByIdQuery Process fail");
                    throw;
                }
                
                return await _service.GetScore(request.Id);
            }
        }
    }
}
