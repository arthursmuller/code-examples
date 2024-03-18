using ExternalEntities.Domain.Dtos;
using ExternalEntities.Domain.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ExternalEntities.API.Application.Features.Queries
{
    public class GetUserScoreByIdQuery : IRequest<GetScoreDto>
    {
        public int Id{ get; }

        public GetUserScoreByIdQuery(int id)
        {
            Id = id;
        }
    }

    public class GetUserScoreByIdHandler : IRequestHandler<GetUserScoreByIdQuery, GetScoreDto>
    {
        private readonly ILogger<GetUserScoreByIdHandler> _logger;
        private readonly IUserService _service;

        public GetUserScoreByIdHandler(
            IUserService service,
            ILogger<GetUserScoreByIdHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<GetScoreDto> Handle(GetUserScoreByIdQuery request, CancellationToken cancellation)
        {
            var userId = request.Id;
            try
            {
                return await _service.GetScore(userId);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "GetUserScoreByIdQuery fail,error:{ex.Message},stackTrace:{ex.StackTrace},id:{id} :", ex.Message, ex.StackTrace, request.Id);
                _logger.LogError(-1, ex, $"GetUserScoreByIdQuery Process fail UserId - {userId}");

                throw;
            }
        }
    }
}
