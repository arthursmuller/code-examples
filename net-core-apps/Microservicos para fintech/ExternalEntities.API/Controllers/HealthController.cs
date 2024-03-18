using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ExternalEntities.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/v1/")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Health() => await Task.FromResult(Ok("Healthy"));
    }
}
