using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Base.Shared.Services.Implementations
{
    /// <summary>
    /// Implementation of 
    /// a Service to show
    /// that Services can't be registered after app.Build() 
    /// :-(
    /// </summary>
    public class LateService : ILateService
    {
        private readonly IExampleHService exampleMService;

        public LateService(IExampleHService exampleSharedService)
        {
            this.exampleMService = exampleSharedService;
        }
        public string Do()
        {
            return exampleMService.Do("Host");
        }
    }
}
