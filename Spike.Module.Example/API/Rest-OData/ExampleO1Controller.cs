using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using App.Modules.Example.Data;
using App.Modules.Example.Models;
using App.Modules.Example.API;
using App.Modules.Example.Services;

namespace App.Modules.Example.Controllers.OData
{
    [Route(ApiConstants.Areas.Module.OData.V1.Routing.Controllers.Controller1.Route)]
    public class ExampleO1Controller : ODataController
    {
        public ExampleO1Controller(IExampleModuleService exampleMService) { 
        }


        [EnableQuery(PageSize = 100)]
        [HttpGet("")]
        [HttpGet("Get")]
        [HttpGet("$count")]
        //[ApiExplorerSettings(GroupName = AppAPIConstants.OpenAPI.Generation.Areas.ModuleA.OData.ID)]
        public IEnumerable<ExampleModuleModel> Get()
        {
            return FakeDataBuilder.Get();
        }
    }
}
