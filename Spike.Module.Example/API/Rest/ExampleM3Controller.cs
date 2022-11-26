using Microsoft.AspNetCore.Mvc;
using App.Modules.Example.API;
using App.Modules.Example.Services;

namespace App.Modules.Example
{
    [ApiController]
    [Route(ApiConstants.Areas.Module.Rest.V1.Routing.Controllers.ExampleM3.Route)]
    public class ExampleM3Controller : ControllerBase
    {
        private readonly IExampleModuleService _exampleMService;

        public ExampleM3Controller(IExampleModuleService exampleMService)
        {
            this._exampleMService = exampleMService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Content(_exampleMService.Do());
        }
    }
}
