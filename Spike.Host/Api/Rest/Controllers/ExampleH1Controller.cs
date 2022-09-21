using Microsoft.AspNetCore.Mvc;
using Spike.Base.Shared.Services.Implementations;

namespace Spike.Base.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExampleH1Controller : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {
            return Content("Hello World.");
        }

    }
}