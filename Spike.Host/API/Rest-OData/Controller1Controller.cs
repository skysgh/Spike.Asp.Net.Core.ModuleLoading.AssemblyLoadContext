using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using App.Base.Data;
using App.Base.API;
using App.Base.Shared.Services;

namespace App.Base.Host.API.OData
{
    [Route(AppAPIConstants.Areas.Base.OData.V1.Routing.Controllers.Controller1.Route)]
    public class ControllerO1Controller : ODataController
    {
        private IExampleHService _exampleHService;

        public ControllerO1Controller(IExampleHService exampleSharedService)
        {
            _exampleHService = exampleSharedService;
        }


        [EnableQuery(PageSize = 100)]
        [HttpGet("")]
        [HttpGet("Get")]
        [HttpGet("$count")]
        //[ApiExplorerSettings(GroupName = AppAPIConstants.OpenAPI.Generation.Areas.ModuleA.OData.ID)]
        public IActionResult Get()
        {
            return Ok(FakeDataBuilder.Get());
        }
    }
}
