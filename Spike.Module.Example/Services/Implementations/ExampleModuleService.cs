using App.Modules.Example.Services;

namespace App.Modules.Example.Services.Implementations
{
    /// <summary>
    /// An instance for
    /// a Module Service 
    /// registered late, when a plugin is loaded,
    /// that is injected
    /// into a Module's Controller's Constructor.
    /// </summary>    
    public class ExampleModuleService : IExampleModuleService
    {
        public ExampleModuleService()
        {

        }
        public string Do()
        {
            return "So there you are...a plugin Controller. Injected with a a Plugin Service. Next stop...an OData Controller...";
        }
    }
}
