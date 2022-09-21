using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Spike.Base.Shared.Services;
using Spike.Base.Shared.Services.Implementations;
using Spike.Module.Example.Services;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace App.Module.Example
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExampleM1Controller: ControllerBase
    {

        public ExampleM1Controller(IServiceProvider sp)
        {
            var c = sp.GetService<IServiceContainer>();
            
            // Appears to contain only default services.
            // as even this service doesn't show up.
            var check = sp.GetService<Spike.Base.Shared.Services.IExampleHService>();
            // And this one, was never registered anywhere
            // so clearly will be null.
            var check2 = sp.GetService<Spike.Module.Example.Services.IExampleMService>();
            // Says its *not* the root scope of the
            // ServiceProvider, but root doesn't list the
            // IExampleMService anyway.
            // Q: 
            // So is Root a different ServiceProvider than Host's Root?
            // Q:
            // if so, how & where & when can we 'teach' it?
        }

        //[HttpGet]
        //public IActionResult Get()
        //{
        //    return Content("Hello World.");
        //}

        //does not work any better(the service is still unknown to
        //the serviceprovider:

        [HttpGet]
        public IActionResult Get([FromServices] Spike.Base.Shared.Services.IExampleHService exampleHService)
        {
            return Content("Hello World.");
        }
    }
}