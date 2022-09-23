using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spike.DotNetCore.ILSpy._01.Services.Configuration
{
    public class ValidationConstraintConfiguration
    {
        public Permitted Permitted {get;}=new Permitted();
        public Excluded Excluded { get; } = new Excluded();

    }

    public class Permitted {
        public List<string> AssemblyNames { get; } = new List<string>();
        public List<string> Namespaces { get; } = new List<string>();
    }
    public class Excluded
    {
        public List<string> AssemblyNames { get; } = new List<string>();
        public List<string> Namespaces { get; } = new List<string>();
        public List<string> Words { get; } = new List<string>();
    }

}
