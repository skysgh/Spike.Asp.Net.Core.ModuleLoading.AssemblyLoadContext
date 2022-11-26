using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Base.Shared.Services
{
    /// <summary>
    /// Contract that all Services
    /// that want to be registered
    /// when late-loaded -- ie from
    /// a Plugin Module -- have to 
    /// implement.
    /// <para>
    /// eg: IExampleModuleService: IModuleService
    /// </para>
    /// </summary>
    public interface IModuleService
    {

    }
}
