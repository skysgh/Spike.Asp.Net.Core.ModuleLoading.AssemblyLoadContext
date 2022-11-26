using Microsoft.AspNetCore.Mvc;
using App.Base.Shared.Services.Implementations;
using App.Base.API;

namespace App.Base.API.Rest.Controllers
{
    [ApiController]
    [Route(AppAPIConstants.Areas.Base.Rest.V1.Routing.Controllers.ExampleH1.Route)]
    public class ExampleH1Controller : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {
            return Content("Hello World (from a Controller without need of DI in Constructor).");
        }

    }
}