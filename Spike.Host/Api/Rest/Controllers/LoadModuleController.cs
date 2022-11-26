using Autofac;
using Autofac.Core;
using Microsoft.AspNetCore.Mvc;
using App.Base.Host.Services;
using System.Reflection;
using App.Base.API;

namespace App.Base.API.Rest.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route(AppAPIConstants.Areas.Base.Rest.V1.Routing.Controllers.LoadModule.Route)]
    [ApiController]
    public class LoadModuleController : ControllerBase
    {
        private readonly IModuleLoadingService _moduleLoadingService;

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
                Path.Combine(discoveryRootDir, "bin/Debug/Net7.0/");

            // Note that I've renamed the assembly (so doesn't match project name):
            string assemblyPath =
                Path.Combine(sourceDir, "App.Modules.Example.dll");

            bool exits = Path.Exists(assemblyPath);

            return _moduleLoadingService.Load(assemblyPath, discoveryRootDir) != null
                ? Content("Module Assembly loaded :-)...Now try invoking the new services...")
                : Content("Module Assembly NOT loaded :-(");
        }

    }


}

