using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Financing.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/v1/")]
    public class HealthController : ControllerBase
    {
        [HttpGet] public async Task<IActionResult> GetMyWallet() => await Task.FromResult(Ok("Healthy"));
    }
}
