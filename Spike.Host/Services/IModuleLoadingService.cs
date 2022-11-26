using App.Base.DI;
using System.Reflection;

namespace App.Base.Host.Services
{
    public interface IModuleLoadingService
    {
        ScopeDictionary Scopes { get; }
        public Assembly Load(string assemblyFilePath, string? assemblyResolutionBaseDirectoryPath = null);
    }
}
