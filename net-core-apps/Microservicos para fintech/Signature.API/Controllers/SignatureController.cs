using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Signature.API;
using Signature.Domain.Dtos;
using Signature.Domain.Services;
using System.Threading.Tasks;

namespace Financing.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/v1/")]
    public class SignatureController : BaseController
    {
        private readonly ISignatureService _svc;
        public SignatureController(ISignatureService svc) => (_svc) = (svc);

        [HttpGet("signature-drawing/{id}")]
        public async Task<IActionResult> GetSignatureBase64([FromRoute] int id)
            => RespondOk(await _svc.GetSignatureUrl(id), "Retrieved");

        [HttpPost("update-signature/{id}")]
        public async Task<IActionResult> UpdateSignature([FromRoute] int id, [FromForm] IFormFile file)
        {
            await _svc.UpdateSignatureDrawing(id, base64(file));
            return Ok();
        }

        [HttpPost("update-profile-picture/{id}")]
        public async Task<IActionResult> UpdateSignaturePicture([FromRoute] int id, [FromForm] IFormFile file)
        {
            await _svc.UpdateSignaturePicture(id, base64(file));
            return Ok();
        }
        [HttpPost("update-created-date")]
        public async Task<IActionResult> UpdateCreatedDate(UpdateSignatureCreatedDateDto dto)
        {
            await _svc.UpdateCreatedDate(dto.Id, dto.Date);
            return Ok();
        }

        [HttpPost("verify")]
        public async Task<IActionResult> GetVerify([FromForm] IFormFile file)
            => RespondOk(await _svc.VerifyPdfFile(base64(file)), "Signature Verified");

        [HttpGet("certificate/{id}")]
        public async Task<IActionResult> VerifySignature([FromRoute] string id)
            => file(await _svc.DownloadCertificate(id));
    }
}
