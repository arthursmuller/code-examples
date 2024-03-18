using ExternalEntities.API.Application.Abstractions;
using ExternalEntities.API.Application.Features.Queries;
using ExternalEntities.Domain.Dtos;
using ExternalEntities.Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ExternalEntities.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/v1/")]
    public class ExternalEntitiesController : BaseController
    {
        private readonly IBusinessService _businessService;
        public ExternalEntitiesController(IMediator mediator, IBusinessService businessService) : base(mediator)
        {
            _businessService = businessService;
        }

        [HttpGet("user/{cpf}")]
        public async Task<IActionResult> GetUser([FromRoute] GetUserByCpfQuery query) =>
            FormatOkResponse(await send(query), "User retrieved");

        [HttpGet("user/score/{cpf}")]
        public async Task<IActionResult> GetUserScore([FromRoute] string cpf) =>
            FormatOkResponse(await send(new GetUserScoreQuery(cpf)), "Score retrieved");
        [HttpGet("user/score-id/{id}")]
        public async Task<IActionResult> GetUserScore([FromRoute] int id) =>
            FormatOkResponse(await send(new GetUserScoreByIdQuery(id)), "Score retrieved");
        [HttpGet("user/score/simulate/{cpf}")]
        public async Task<IActionResult> Simulate([FromRoute] string cpf) =>
            FormatOkResponse(await send(new SimulateUserScoreQuery(cpf)), "Score retrieved");

        [HttpGet("user/score-id/simulate/{id}")]
        public async Task<IActionResult> Simulate([FromRoute] int id) =>
            FormatOkResponse(await send(new SimulateUserScoreByIdQuery(id)), "Score retrieved");
        
        [HttpPost("business/analysis")]
        public async Task<IActionResult> Analysis(PayiedAnalysisDto dto) =>
            FormatOkResponse(await _businessService.PayiedAnalysis(dto), "Analysis retrieved");
    }
}
