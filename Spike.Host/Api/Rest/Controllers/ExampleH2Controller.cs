using Microsoft.AspNetCore.Mvc;
using App.Base.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using App.Base.API;

namespace App.Base.API.Rest.Controllers
{
    [Route(AppAPIConstants.Areas.Base.Rest.V1.Routing.Controllers.ExampleH2.Route)]
    public class ExampleH2Controller : ODataController
    {
        private readonly IExampleHService exampleHService;

        public ExampleH2Controller(IExampleHService exampleSharedService)
        {
            exampleHService = exampleSharedService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Content(exampleHService.Do("a Controller with a little DI used in Constructor"));
        }
    }
}
