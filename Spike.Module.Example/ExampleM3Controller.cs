using Microsoft.AspNetCore.Mvc;
using Spike.Base.Shared.Services;
using Spike.Module.Example.Services;
using Spike.Module.Example.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Module.Example
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExampleM3Controller : ControllerBase
    {
        private readonly IExampleMService _exampleMService;

        public ExampleM3Controller(IExampleMService exampleMService)
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
