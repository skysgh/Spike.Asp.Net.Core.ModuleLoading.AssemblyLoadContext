using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace Spike.Base.Host.AppDomains
{

    /// <summary>
    /// App Module Custom 
    /// <see cref="AssemblyLoadContext"/>
    /// </summary>
    public class AppModuleLoadContext : AssemblyLoadContext
    {
        private AssemblyDependencyResolver _resolver;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pluginPath"></param>
        public AppModuleLoadContext(string pluginPath)
        {
            _resolver = new AssemblyDependencyResolver(pluginPath);
        }
        /// <inheritdoc/>

        protected override Assembly? Load(AssemblyName assemblyName)
        {
            string? assemblyPath = _resolver.ResolveAssemblyToPath(assemblyName);
            if (assemblyPath != null)
            {
                return LoadFromAssemblyPath(assemblyPath);
            }
            return null;
        }
        /// <inheritdoc/>

        protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
        {
            string? libraryPath = _resolver.ResolveUnmanagedDllToPath(unmanagedDllName);
            if (libraryPath != null)
            {
                return LoadUnmanagedDllFromPath(libraryPath);
            }
            return IntPtr.Zero;
        }
    }
}
