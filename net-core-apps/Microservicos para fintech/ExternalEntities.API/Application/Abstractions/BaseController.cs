using ExternalEntities.Domain.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ExternalEntities.API.Application.Abstractions
{
    public class BaseController : Controller
    {
        protected readonly IMediator _mediator;
        public BaseController(IMediator mediator) =>
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        protected IActionResult FormatOkResponse<T>(T objRetorno, string message = null)
        {
            return Ok(new DefaultResponse<T>(objRetorno) { Message = message });
        }

        protected IActionResult FormatOkResponse<T>(T objRetorno)
        {
            return Ok(objRetorno);
        }

        protected IActionResult FormatCreatedResponse<T>(T objRetorno, string message = null)
        {
            return CreatedAtAction(nameof (objRetorno), new DefaultResponse<T>() { Message = message });
        }

        protected IActionResult FormatBadRequestResponse(string message)
        {
            return BadRequest(new DefaultResponse<object>() { Message = message });
        }

        protected async Task<T> send<T>(IRequest<T> request) where T : class
            =>  await _mediator.Send(request);

        protected async Task<T> sendPrimivite<T>(IRequest<T> request) where T : struct
            => await _mediator.Send(request);

        protected async Task<bool> send(IRequest request)
        {
            await _mediator.Send(request);
            return default;
        }
    }
}


