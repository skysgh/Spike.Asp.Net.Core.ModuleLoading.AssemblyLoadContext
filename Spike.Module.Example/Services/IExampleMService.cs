using Spike.Base.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spike.Module.Example.Services
{
    public interface IExampleMService : IModuleService
    {
        string Do();
    }
}
