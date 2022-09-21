using System.Linq.Expressions;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace Spike.Base.Host.Api.Infrastructure
{
    // Summary:
    //     Represents a default implementation of an System.Web.Http.Dispatcher.IHttpControllerActivator.
    //     A different implementation can be registered via the System.Web.Http.Services.DependencyResolver.
    //     We optimize for the case where we have an System.Web.Http.Controllers.ApiControllerActionInvoker
    //     instance per System.Web.Http.Controllers.HttpControllerDescriptor instance but
    //     can support cases where there are many System.Web.Http.Controllers.HttpControllerDescriptor
    //     instances for one System.Web.Http.Controllers.ApiControllerActionInvoker as well.
    //     In the latter case the lookup is slightly slower because it goes through the
    //     HttpControllerDescriptor.Properties dictionary.
    public class FromDefaultHttpControllerActivator : IHttpControllerActivator
    {
        private Tuple<HttpControllerDescriptor, Func<IHttpController>> _fastCache;

        private object _cacheKey = new object();

        //
        // Summary:
        //     Creates the System.Web.Http.Controllers.IHttpController specified by controllerType
        //     using the given request.
        //
        // Parameters:
        //   request:
        //     The request message.
        //
        //   controllerDescriptor:
        //     The controller descriptor.
        //
        //   controllerType:
        //     The type of the controller.
        //
        // Returns:
        //     An instance of type controllerType.
        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            if (request == null)
            {
                throw new Exception("Argument null");
            }

            if (controllerDescriptor == null)
            {
                throw new Exception("Argument null");
            }

            if (controllerType == null)
            {
                throw new Exception("Argument null");
            }

            try
            {
                Func<IHttpController> activator;
                object value2;
                if (_fastCache == null)
                {
                    IHttpController instanceOrActivator = 
                        GetInstanceOrActivator(request, controllerType, out activator);

                    if (instanceOrActivator != null)
                    {
                        return instanceOrActivator;
                    }

                    Tuple<HttpControllerDescriptor, Func<IHttpController>> value = 
                        Tuple.Create(controllerDescriptor, activator);

                    Interlocked.CompareExchange(ref _fastCache, value, null);
                }
                else if (_fastCache.Item1 == controllerDescriptor)
                {
                    activator = _fastCache.Item2;
                }
                else if (controllerDescriptor.Properties.TryGetValue(_cacheKey, out value2))
                {
                    activator = (Func<IHttpController>)value2;
                }
                else
                {
                    IHttpController instanceOrActivator2 = GetInstanceOrActivator(request, controllerType, out activator);
                    if (instanceOrActivator2 != null)
                    {
                        return instanceOrActivator2;
                    }

                    controllerDescriptor.Properties.TryAdd(_cacheKey, activator);
                }

                return activator();
            }
            catch (Exception innerException)
            {
                throw new Exception("Invalid Op");
            }
        }

        private static IHttpController GetInstanceOrActivator(HttpRequestMessage request, Type controllerType, out Func<IHttpController> activator)
        {
            IHttpController httpController = (IHttpController)request.GetDependencyScope().GetService(controllerType);

            if (httpController != null)
            {
                activator = null;
                return httpController;
            }

            activator = TypeActivator.Create<IHttpController>(controllerType);

            return null;
        }

        //
        // Summary:
        //     Initializes a new instance of the System.Web.Http.Dispatcher.DefaultHttpControllerActivator
        //     class.
        public FromDefaultHttpControllerActivator()
        {
        }
    }

    internal static class TypeActivator
    {
        public static Func<TBase> Create<TBase>(Type instanceType) where TBase : class
        {
            return Expression.Lambda<Func<TBase>>(
                    Expression.New(instanceType), 
                    new ParameterExpression[0]).Compile();
        }

        public static Func<TInstance> Create<TInstance>() where TInstance : class
        {
            return Create<TInstance>(typeof(TInstance));
        }

        public static Func<object> Create(Type instanceType)
        {
            return Create<object>(instanceType);
        }
    }

}


