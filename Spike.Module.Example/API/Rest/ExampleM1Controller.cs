using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using App.Base.Shared.Services;
using System.ComponentModel.Design;
using App.Modules.Example.Services;

namespace App.Modules.Example.API.Rest
{
    [ApiController]
    [Route(ApiConstants.Areas.Module.Rest.V1.Routing.Controllers.ExampleM1.Route)]
    public class ExampleM1Controller : ControllerBase
    {

        public ExampleM1Controller(IServiceProvider sp)
        {
            //Security Check
            // Put this in and see if it picks it up:
            //System.IO.StreamReader? sr=null;
            //using (System.IO.FileStream fs = new FileStream(null, FileAccess.Read))
            //{
            //    sr = new System.IO.StreamReader(fs);
            //}
            //System.IO.Directory.Exists(this.GetType().Assembly.Location);


            var c = sp.GetService<IServiceContainer>();

            // Appears to contain only default services.
            // as even this service doesn't show up.
            var check = sp.GetService<IExampleHService>();
            // And this one, was never registered anywhere
            // so clearly will be null.
            var check2 = sp.GetService<IExampleModuleService>();
            // Says its *not* the root scope of the
            // ServiceProvider, but root doesn't list the
            // IExampleMService anyway.
            // Q: 
            // So is Root a different ServiceProvider than Host's Root?
            // Q:
            // if so, how & where & when can we 'teach' it?
        }


        [HttpGet]
        public IActionResult Get([FromServices] IExampleHService exampleHService)
        {
            return Content("Hello World, injected with a *Host* service.");
        }
    }
}