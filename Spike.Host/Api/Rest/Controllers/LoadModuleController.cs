using Autofac;
using Autofac.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Spike.Base.Host.Api.Infrastructure;
using Spike.Base.Host.AppDomains;
using Spike.Base.Host.AssemblyLoadContexts;
using Spike.Base.Host.Services;
using Spike.Base.Shared.Services;
using System.ComponentModel.Design;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;

namespace Spike.Base.Host.Api.Rest.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LoadModuleController : ControllerBase
    {
        private readonly IModuleLoadingService _moduleLoadingService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="applicationPartManager"></param>
        public LoadModuleController(IModuleLoadingService _moduleLoadingService)
        {
            
            this._moduleLoadingService = _moduleLoadingService;
        }

        /// <summary>
        /// REST API GET Verb handler
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            string discoveryRootDir =
                Path.Combine(
                    Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location),
                    "../../../MODULES/");

            var sourceDir = 
                Path.Combine(discoveryRootDir, "bin/Debug/Net6.0/");

            string assemblyPath =
                Path.Combine(sourceDir, "Spike.Module.Example.dll");

            return _moduleLoadingService.Load(assemblyPath, discoveryRootDir) != null
                ? Content("Module Assembly loaded :-)")
                : Content("Module Assembly NOT loaded :-(");
        }

    }


}

