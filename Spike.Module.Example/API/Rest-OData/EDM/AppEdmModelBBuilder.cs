using App.Modules.Example.Models;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace App.Modules.Example.API.OData.ModelBuilders
{
    public static class AppEdmModelBBuilder
    {
        public static IEdmModel BuildModel()
        {
            var builder = new ODataConventionModelBuilder();

            /*01*/
            // Works, follows convention of part-part == controller name prefix 
            builder.EntitySet<ExampleModuleModel>(
                ApiConstants.Areas.Module.OData.V1.Routing.Controllers.Controller1.Name);

            return builder.GetEdmModel();

        }


    }
}
