using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Dependencies;

namespace Spike.Base.Host.Api.Infrastructure
{
    public class CustomControllerActivator : IControllerActivator
    {

        public CustomControllerActivator( IServiceProvider serviceProvider)
        {
            // SEE HERE:
            // https://stackoverflow.com/questions/68361519/asp-net-core-3-overriding-the-default-controllerfactory-on-custom-how-save-it
            ServiceProvider = serviceProvider;
        }

        public IServiceProvider ServiceProvider { get; }

        public object Create(ControllerContext context)
        {
            Type type = context.ActionDescriptor.ControllerTypeInfo.AsType();

            return Activator.CreateInstance(type);
            //var result = ServiceProvider.GetService(type);
            //return result;
            ////var serviceProvider = controllerContext.HttpContext.RequestServices;
            //return _typeActivatorCache.CreateInstance<object>(serviceProvider, controllerTypeInfo.AsType());
        }

        public void Release(ControllerContext context, object controller)
        {
        }
    }
}
