using System;
using System.Collections.Generic;

namespace Solomonic.DependencyInjection.Lifecycles
{
    public interface ILifecycle
    {
        object GetInstance(DependencyKey dependencyKey,  DependencyContainer dependencyContainer, Type[] ctorTypes, IList<DependencyArgument> arguments, Func<DependencyContainer, object> initFunc);
        void Destroy(DependencyKey dependencyKey);
        void SetInstance(DependencyKey dependencyKey, object instance);
    }
}