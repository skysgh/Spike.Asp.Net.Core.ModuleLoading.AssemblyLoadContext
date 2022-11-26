using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using App.Base.Shared.Services;
using App.Base.Shared.Services.Implementations;
using System.Reflection;
using Autofac;
using Grace.AspNetCore.Hosting;
using Grace.DependencyInjection;
using Autofac.Core;
using Microsoft.AspNetCore.Mvc.Controllers;
using App.Base.Host.Services;
using App.Base.Host.Services.Implementations;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.OData;
using App.Base.API.OData.ModelBuilders;
using App.Base.API;
using App.Base.MVC.Infrastructure;
using App.Base.MVC.Controllers;

namespace App.ModuleLoadingAndDI
{
    public class Program
    {

        private static CancellationTokenSource 
            cancelTokenSource = 
            new System.Threading.CancellationTokenSource();

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // IMPORTANT:
            // Replacing default DI service with Autofac
            // as it permits child scopes...which is necessary
            // to manage new services imported from 3rd party
            // modules.
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

            // Demo of registering at startup some service needed by controllers:
            builder.Services.AddSingleton<IExampleHService, ExampleHService>();

            RegisterServicesNeededToImport3rdPartyModulesAndTheirControllersAndServices(builder);

            builder.Services
                .AddControllersWithViews()
                //Force this on to ensure DI works after modules are loaded
                .AddControllersAsServices()
                //But OData works as always:
                .AddOData(
                                opt =>
                                opt.Count()
                                .Filter()
                                .Expand()
                                .Select()
                                .OrderBy()
                                .SetMaxTop(5)
                                //Add Base EDM:
                                .AddRouteComponents(
                                    AppAPIConstants.Areas.Base.OData.V1.Routing.RoutePrefix,
                                    AppModuleBaseEdmModelBuilder.BuildModel())
                                // This is on by default, but still...
                                .EnableAttributeRouting = true
                );

            AddReplacementFactoryOfControllersThatIsAwareOfChildDIScopes(builder);

            // Wire up custom Resetter invoked by upload controller.
            AddActionDescriptorChangeProvider(builder);

            // That was the last chance to add anthing before Build is called:
            // =======================================================
            // =======================================================
            var app = builder.Build();
            // =======================================================
            // =======================================================

            // IE: This won't work now:
            // builder.Services.AddSingleton<ILateService, LateService>();
            // Returns null:
            var x2 = app.Services.GetService<ILateService>();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment()){app.UseHsts();}
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            // Register default route for WebAPI controllers :
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action=Index}/{id?}");
            
            // Note that OData Routes were registered earlier
            // than build(), as Config information.
            // NOTE: Maybe that that will be the way to register them later...
            // Either way, add other odata specific handling to debug:
            // Provide the ~/$odata route:
            app.UseODataRouteDebug();
            app.UseODataQueryRequest();

            // This page will load the Angular page:
            app.MapFallbackToFile("index.html");

            app.Run();
        }

        private static void AddReplacementFactoryOfControllersThatIsAwareOfChildDIScopes(WebApplicationBuilder builder)
        {
            builder.Services.Add(ServiceDescriptor.Transient<IControllerActivator, AppServiceBasedControllerActivator>());
        }

        private static void RegisterServicesNeededToImport3rdPartyModulesAndTheirControllersAndServices(WebApplicationBuilder builder)
        {
            // Register two services needed to process the
            // loading of 3rd party modules:
            builder.Services.AddSingleton<IPluginValidationService, PluginValidationService>();
            builder.Services.AddSingleton<IModuleLoadingService, ModuleLoadingService>();
        }

        private static void AddActionDescriptorChangeProvider(WebApplicationBuilder builder)
        {
            //Replace:
            builder.Services.AddSingleton<IActionDescriptorChangeProvider>(AppActionDescriptorChangeProvider.Instance);
            //builder.Services.AddSingleton(AppActionDescriptorChangeProvider.Instance);
        }

    }
}