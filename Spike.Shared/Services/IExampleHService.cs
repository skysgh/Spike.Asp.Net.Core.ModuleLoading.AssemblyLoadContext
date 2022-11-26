namespace App.Base.Shared.Services
{
    /// <summary>
    // Contract for
    // a Service registered at startup
    // and injected into Controllers
    /// </summary>
    public interface IExampleHService
    {
        string Do(string sourceInfo);
    }

}