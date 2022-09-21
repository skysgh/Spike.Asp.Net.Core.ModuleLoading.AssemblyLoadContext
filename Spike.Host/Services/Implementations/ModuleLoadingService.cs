using Autofac;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Spike.Base.Host.Api.Infrastructure;
using Spike.Base.Host.AppDomains;
using Spike.Base.Host.AssemblyLoadContexts;
using Spike.Base.Shared.Services;
using System.Reflection;

namespace Spike.Base.Host.Services.Implementations
{
    public class ModuleLoadingService : IModuleLoadingService
    {
        private readonly ApplicationPartManager _applicationPartManager;
        private readonly ILifetimeScope _lifetimeScope;
        private readonly IHostApplicationLifetime _applicationLifetime;

        public ScopeDictionary Scopes
        {
            get
            {
                return ScopeDictionary.Instance;
            }
        }

        public ModuleLoadingService(
            IHostApplicationLifetime applicationLifetime,
            ApplicationPartManager applicationPartManager,
            ILifetimeScope lifetimeScope)
        {
            _applicationPartManager = applicationPartManager;
            this._lifetimeScope = lifetimeScope;
            _applicationLifetime = applicationLifetime;
        }


        public Assembly Load(string assemblyFilePath, string? assemblyResolutionBaseDirectoryPath=null)
        {
            if (string.IsNullOrEmpty(assemblyResolutionBaseDirectoryPath))
            {
                assemblyResolutionBaseDirectoryPath = Path.GetDirectoryName(assemblyFilePath);
            }
            var loadContext = new AppModuleLoadContext(assemblyResolutionBaseDirectoryPath);

            //var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(assemblyPath);
            var assembly = loadContext.LoadFromAssemblyPath(assemblyFilePath);


            if (assembly == null)
            {
                return null;
            }

            _applicationPartManager
                .ApplicationParts
                .Add(new AssemblyPart(assembly));

            ILifetimeScope scope = 
                RegisterDependenciesInDIScope(loadContext, assembly);

            // Notify change so that next request
            // knows about new Controllers:
            AppActionDescriptorChangeProvider.Instance.Reset();
            // TODO: This turns off/on the whole server.
            // _applicationLifetime.StopApplication();

            return assembly;
        }

     

        private ILifetimeScope RegisterDependenciesInDIScope(AppModuleLoadContext context, Assembly assembly)
        {
            ContainerBuilder builderUsed;

            List<Type> serviceTypes = new List<Type>();
            List<Type> controllerTypes = new List<Type>();

            // Now go through Types:
            ILifetimeScope moduleScope = _lifetimeScope.BeginLifetimeScope(builder =>
            {
                builderUsed = builder;
                // = builder.Build();


                // TODO: Primitive/improvable way to look for Services
                var assemblyTypes = assembly.GetTypes();
                //Search for Services first:
                foreach (var serviceType in assemblyTypes)
                {
                    if (serviceType.IsInterface)
                    {
                        continue;
                    }
                    if (!typeof(IModuleService).IsAssignableFrom(serviceType))
                    {
                        continue;
                    }
                    var tInterface = serviceType.GetInterfaces().First();
                    // If ONLY WE COULD DO THIS NOW!!!!
                    //_serviceCollection.AddSingleton(tInterface, type);
                    //instead, add to builder:
                    builderUsed.RegisterType(serviceType).As(tInterface);
                    serviceTypes.Add(tInterface);
                }
                //Search for and register controllers:
                foreach (var controllerType in assemblyTypes)
                {
                    if (typeof(ControllerBase).IsAssignableFrom(controllerType))
                    {
                        controllerTypes.Add(controllerType);
                        builderUsed.RegisterType(controllerType);
                    }
                }
            });

            var lastInterfaceTypeRegistered = serviceTypes.Last();
            if (lastInterfaceTypeRegistered != null)
            {
                object tmpInstance;
                bool r = moduleScope.TryResolve(lastInterfaceTypeRegistered, out tmpInstance);
            }

            foreach(Type controllerType in controllerTypes)
            {
                III info = new III() {
                    Context = context,
                    Assembly = assembly,
                    Scope = moduleScope  
                };
                // Save the scope so that it doesn't get Disposed.
                // We'll try to use it again from a Controller creator.
                ScopeDictionary.Instance[controllerType] = info;
            }
            // Return the Scope being created
            return moduleScope;
        }
    }
}
