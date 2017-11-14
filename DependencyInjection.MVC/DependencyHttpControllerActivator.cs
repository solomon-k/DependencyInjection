using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace Solomonic.DependencyInjection.MVC
{
    public class DependencyHttpControllerActivator : IHttpControllerActivator
    {
        private readonly DefaultHttpControllerActivator _defaultHttpControllerActivator = new DefaultHttpControllerActivator();
        private readonly DependencyContainer _dependencyContainer;
        public DependencyHttpControllerActivator(DependencyContainer dependencyContainer)
        {
            _dependencyContainer = dependencyContainer;
        }

        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            try
            {
                if (controllerType == null)
                {
                    return _defaultHttpControllerActivator.Create(request, controllerDescriptor, null);
                }
            }
            catch
            {
                return null;
            }
            

            IHttpController controller = _dependencyContainer.CreateInstance(controllerType) as IHttpController;
            return controller;
        }
    }
}