using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Solomonic.DependencyInjection.MVC
{
    public class DependencyControllerFactory : DefaultControllerFactory
    {
        private readonly DependencyContainer _dependencyContainer;

        public DependencyControllerFactory(DependencyContainer dependencyContainer)
        {
            _dependencyContainer = dependencyContainer;
        }

        protected override IController GetControllerInstance(RequestContext requestContext,
            Type controllerType)
        {
            try
            {
                if (controllerType == null) return base.GetControllerInstance(requestContext, null);
            }
            catch (Exception)
            {
                //ignore
                return null;
            }

            IController controller = _dependencyContainer.CreateInstance(controllerType) as IController;
            return controller;
        }
    }
}
