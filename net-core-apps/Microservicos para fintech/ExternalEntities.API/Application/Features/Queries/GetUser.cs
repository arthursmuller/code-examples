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
    public class GetUserScoreQuery : IRequest<GetScoreDto>
    {
        public string Cpf { get; }

        public GetUserScoreQuery(string cpf)
        {
            Cpf = cpf;
        }
    }

    public class GetUserScoreHandler : IRequestHandler<GetUserScoreQuery, GetScoreDto>
    {
        private readonly ILogger<GetUserScoreHandler> _logger;
        private readonly IUserService _service;

        public GetUserScoreHandler(
            IUserService service,
            ILogger<GetUserScoreHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<GetScoreDto> Handle(GetUserScoreQuery request, CancellationToken cancellation)
        {
            try
            {
                return await _service.GetScore(request.Cpf);
            }
            catch (Exception ex)
            {
                if (!(ex is DbUpdateException))
                {
                    _logger.LogInformation(ex, "GetUserScoreQuery fail,error:{ex.Message},stackTrace:{ex.StackTrace},cpf:{cpf} :", ex.Message, ex.StackTrace, request.Cpf);
                    _logger.LogError(-1, ex, "GetUserScoreQuery Process fail");
                    throw;
                }
                
                return await _service.GetScore(request.Cpf);
            }
        }
    }
}
