using Spike.Base.Shared.Services;

namespace Spike.Base.Shared.Services.Implementations
{
    public class ExampleHService : IExampleHService
    {
        public string Do(string sourceInfo)
        {
            return $"Hello Fabulous World.(from {sourceInfo})";
        }

    }

}