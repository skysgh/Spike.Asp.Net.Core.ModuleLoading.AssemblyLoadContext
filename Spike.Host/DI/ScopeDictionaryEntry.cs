using Autofac;
using App.Base.Host.AppDomains;
using System.Reflection;

namespace App.Base.DI
{
    public class ScopeDictionaryEntry
    {
        // The Assembly context that can be dropped later
        public AppModuleLoadContext Context { get; set; }
        public Assembly Assembly { get; set; }
        public ILifetimeScope Scope { get; set; }
    }
}
