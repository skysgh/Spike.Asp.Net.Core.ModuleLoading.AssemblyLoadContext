using Autofac;
using Spike.Base.Host.AppDomains;
using System.Reflection;

namespace Spike.Base.Host.AssemblyLoadContexts
{

    public class III {
        public AppModuleLoadContext Context { get; set; }
        public Assembly Assembly { get; set; }
        public ILifetimeScope Scope { get; set; }
    }

    public class ScopeDictionary: Dictionary<Type, III>
    {
        static public ScopeDictionary Instance { get; private set; }=new ScopeDictionary(); 
    }
}
