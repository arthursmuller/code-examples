
using ExternalEntities.Domain.Dtos;
using ExternalEntities.Domain.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ExternalEntities.API.Application.Features.Queries
{
    public record GetUserByCpfQuery(string Cpf): IRequest<UserDto>;

    public class GetUserByCpfHandler : IRequestHandler<GetUserByCpfQuery, UserDto>
    {
        private readonly ILogger<GetUserByCpfHandler> _logger;
        private readonly IUserService _service;

        public GetUserByCpfHandler(
            IUserService service,
            ILogger<GetUserByCpfHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<UserDto> Handle(GetUserByCpfQuery request, CancellationToken cancellation)
        {
            var cpf = request.Cpf;
            try
            {
                return await _service.Get(cpf);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "GetUserByCpfQuery fail,error:{ex.Message},stackTrace:{ex.StackTrace},id:{id} :", ex.Message, ex.StackTrace, cpf);
                _logger.LogError(-1, ex, $"GetUserByCpfQuery Process fail cpf - {cpf}");

                throw;
            }
        }
    }
}
