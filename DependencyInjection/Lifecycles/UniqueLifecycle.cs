using System;
using System.Collections.Generic;

namespace Solomonic.DependencyInjection.Lifecycles
{
    public class UniqueLifecycle : ILifecycle
    {
        public object GetInstance(DependencyKey key, DependencyContainer dependencyContainer, Type[] ctorTypes, IList<DependencyArgument> arguments, Func<DependencyContainer, object> initFunc)
        {
            return initFunc == null
                ? DependencyActivator.GetInstance(key.Type, dependencyContainer, ctorTypes, arguments)
                : initFunc(dependencyContainer);
        }

        public void Destroy(DependencyKey dependencyKey)
        {
            //ignore
        }

        public void SetInstance(DependencyKey dependencyKey, object instance)
        {
            //ignore
        }
    }
}