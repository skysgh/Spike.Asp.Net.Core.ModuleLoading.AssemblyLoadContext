using Spike.Base.Host.AssemblyLoadContexts;
using System.Reflection;

namespace Spike.Base.Host.Services
{
    public interface IModuleLoadingService
    {
        ScopeDictionary Scopes { get; }
        public Assembly Load(string assemblyFilePath, string? assemblyResolutionBaseDirectoryPath = null);
    }
}
