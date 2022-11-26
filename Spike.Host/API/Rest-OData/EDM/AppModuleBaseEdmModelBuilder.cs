using App.Base.Shared.Models;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace App.Base.API.OData.ModelBuilders
{
    public static class AppModuleBaseEdmModelBuilder
    {
        public static IEdmModel BuildModel()
        {
            var builder = new ODataConventionModelBuilder();

            /*01*/
            // Works, follows convention of part-part == controller name prefix 
            // a) found as an odata controller (under api/odata/v{version}
            // b) acting as an Odata controller (returning odata wrapper in json)
            // c) Queryability works
            builder.EntitySet<SomeBaseParentModel>(AppAPIConstants.Areas.Base.OData.V1.Routing.Controllers.Controller1.Name);

            // ie...what the hell is going on?!?
            return builder.GetEdmModel();
        }
    }
}
