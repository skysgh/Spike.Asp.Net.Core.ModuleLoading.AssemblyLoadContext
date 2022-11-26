using Spike.DotNetCore.ILSpy._01.Services.Configuration;

namespace App.Base.Host.Services
{
    /// <summary>
    /// Service to validate a plugin
    /// before loading it up into the same memory.
    /// </summary>
    public interface IPluginValidationService
    {
        /// <summary>
        /// Validate the Assembly as being 
        /// maybe ok as a Plugin
        /// </summary>
        /// <param name="assemblyFilePath"></param>
        /// <param name="validationConstraintConfiguration"></param>
        /// <returns></returns>
        bool ValidateAssembly(string assemblyFilePath, ValidationConstraintConfiguration validationConstraintConfiguration = null, string baseSearchDir = null);
    }
}
