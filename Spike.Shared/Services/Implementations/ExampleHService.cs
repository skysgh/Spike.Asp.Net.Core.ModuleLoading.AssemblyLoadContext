using App.Base.Shared.Services;

namespace App.Base.Shared.Services.Implementations
{
    /// <summary>
    // Implementation of
    // a Service registered at startup
    // and injected into Controllers
    /// </summary>
    public class ExampleHService : IExampleHService
    {
        public string Do(string sourceInfo)
        {
            return $"Hello Fabulous World.(from {sourceInfo})";
        }

    }

}