
namespace App.Base.Shared.Services
{
    /// <summary>
    /// Contract for 
    /// a Service to show
    /// that Services can't be registered after app.Build() 
    /// :-(
    /// </summary>
    public interface ILateService
    {
        string Do();
    }
}
