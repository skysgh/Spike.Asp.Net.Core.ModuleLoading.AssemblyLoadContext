using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Spike.Base.Host.Api.Infrastructure;
using Spike.Base.Shared.Services;
using Spike.Base.Shared.Services.Implementations;
using System.Reflection;
using Autofac;
using Grace.AspNetCore.Hosting;
using Grace.DependencyInjection;
using Autofac.Core;
using Microsoft.AspNetCore.Mvc.Controllers;
using Spike.Base.Host.Services;
using Spike.Base.Host.Services.Implementations;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Spike.ModuleLoadingAndDI
{
    public class Program
    {

        private static CancellationTokenSource 
            cancelTokenSource = 
            new System.Threading.CancellationTokenSource();

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);



            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            //builder.Host.UseGrace();

            //Add a service
            builder.Services.AddSingleton<IExampleHService, ExampleHService>();
            builder.Services.AddSingleton<IModuleLoadingService, ModuleLoadingService>();
            // Add services to the container.
            builder.Services.AddControllersWithViews()
           //no!!!! Fails for Modules
           .AddControllersAsServices()
           ;
            // Wire up custom Resetter invoked by upload controller.
            AddActionDescriptorChangeProvider(builder);


            // builder.Services.AddSingleton(typeof(IControllerActivator),
            // typeof(CustomControllerActivator));

            //builder.Services.Replace(ServiceDescriptor.Transient<IControllerActivator, MyServiceBasedControllerActivator>());
            builder.Services.Add(ServiceDescriptor.Transient<IControllerActivator, MyServiceBasedControllerActivator>());


            //That was the last chance to add anthing before Build is called:
            var app = builder.Build();
            //Works, no problem:
            var x1 = app.Services.GetService<IExampleHService>();
            // Adding additional Services after the build event:
            // Will raise Exception
            // Not fixable by setting the flag by refletion, as it's too late.
            //builder.Services.GetType()
            //    .GetProperties(
            //    BindingFlags.Public|BindingFlags.SetProperty|BindingFlags.Instance
            //    )
            //    .Where(x => x.Name == "IsReadOnly")
            //    .First()
            //    .SetValue(builder.Services, false);
            // Without using reflection to re-permit adding services without raising
            // Exception "Cannot modify ServiceCollection after application is built."
            // it's still too late (Host already built, so no longer looking at source info)
            try
            {
                builder.Services.AddSingleton<ILateService, LateService>();
            }catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            // Calling build twice also raises an exception:
            // builder.Build();
            // So...if we load a new DLL into a new AppDomainLoadContext,
            // that contains a Controller
            // that has a dependency on a Service, 
            // how do we teach the DI (IServiceProvider) how to build
            // it, and dependency service first???
            
            // Returns null - *** NOT *** what we want...: 
            var x2 = app.Services.GetService<ILateService>();

            //That too doesn't help (the Update method is obsolete).
            //AutofacServiceProvider sp = (AutofacServiceProvider)app.Services;
            ILifetimeScope autofacContainer = app.Services.GetAutofacRoot();
            


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action=Index}/{id?}");

            app.MapFallbackToFile("index.html");

            app.Run ();
        }


        private static void AddActionDescriptorChangeProvider(WebApplicationBuilder builder)
        {
            //Replace:
            builder.Services.AddSingleton<IActionDescriptorChangeProvider>(AppActionDescriptorChangeProvider.Instance);
            //builder.Services.AddSingleton(AppActionDescriptorChangeProvider.Instance);
        }

    }
}