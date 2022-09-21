using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Dispatcher;
using System.Web.Http.Controllers;
using System.Web.Http.Dependencies;
using Spike.Base.Host.AssemblyLoadContexts;
using Autofac;

namespace Spike.Base.Host.Api.Infrastructure
{
    // See: https://andrewlock.net/controller-activation-and-dependency-injection-in-asp-net-core-mvc/
    public class MyServiceBasedControllerActivator : IControllerActivator
    {


        //DefaultHttpControllerActivator
        public object Create(ControllerContext actionContext)
        {
            var controllerType = actionContext.ActionDescriptor.ControllerTypeInfo.AsType();



            // Use the default DI solution:
            var httpController = actionContext.HttpContext.RequestServices.GetService(controllerType);

            // And if it was...
            if (httpController != null)
            {
                return httpController;
            }
            III info;
            ScopeDictionary.Instance.TryGetValue(controllerType, out info);

            if (info != null)
            {
                info.Scope.TryResolve(controllerType, out httpController);
                return httpController;
            }
            try
            {
                httpController = System.Activator.CreateInstance(controllerType);
                return httpController;
            }
            catch (Exception ex)
            {
                // Fail to another handler since
                // we didn't replace ,we added...?

                return null;
            }


        }

        public virtual void Release(ControllerContext context, object controller)
        {
        }
    }
}
