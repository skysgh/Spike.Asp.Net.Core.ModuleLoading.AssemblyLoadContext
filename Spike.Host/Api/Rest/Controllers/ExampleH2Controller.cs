using Microsoft.AspNetCore.Mvc;
using Spike.Base.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spike.Base.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExampleH2Controller : ControllerBase
    {
        private readonly IExampleHService exampleHService;

        public ExampleH2Controller(IExampleHService exampleSharedService)
        {
            this.exampleHService = exampleSharedService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Content(exampleHService.Do("Host"));
        }
    }
}
