using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spike.Base.Shared.Services.Implementations
{
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
