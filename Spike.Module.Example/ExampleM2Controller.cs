using Microsoft.AspNetCore.Mvc;
using Spike.Base.Shared.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Module.Example
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExampleM2Controller : ControllerBase
    {
        private readonly IExampleHService _exampleHService;

        // This FINALLY WORKS because 
        // The Reference to the Shared Dll in the *.csproj
        // is now marked with <Private>false</Private> so
        // it doesn't copy the referenced DLL to the target.
        // Refer to:
        // https://learn.microsoft.com/en-us/dotnet/core/tutorials/creating-app-with-plugin-support
        public ExampleM2Controller(IExampleHService exampleHService)
        {
            this._exampleHService = exampleHService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Content(_exampleHService.Do("Module!"));
        }
    }
}
