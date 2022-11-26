
using App.Base.Shared.Services;

namespace App.Modules.Example.Services
{
    /// <summary>
    /// A Contract for
    /// a Module Service 
    /// registered late, when a plugin is loaded,
    /// that is injected
    /// into a Module's Controller's Constructor.
    /// </summary>
    /// <remarks>
    /// IMPORTANT: 
    /// It's a precondition of 
    /// <see cref="IModuleLoadingService"/>
    /// that 
    /// Services MUST inherit from IModuleService
    /// to be considered as something to register.
    /// </remarks>
    public interface IExampleModuleService : IModuleService
    {
        string Do();
    }
}
