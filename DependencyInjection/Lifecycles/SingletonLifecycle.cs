using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Solomonic.DependencyInjection.Lifecycles
{
    public class SingletonLifecycle : ILifecycle
    {
        private static readonly IDictionary<DependencyKey, object> Objects = new ConcurrentDictionary<DependencyKey, object>();
        public object GetInstance(DependencyKey key, DependencyContainer dependencyContainer, Type[] ctorTypes, IList<DependencyArgument> arguments, Func<DependencyContainer, object> initFunc)
        {
            lock (Objects)
            {
                if (Objects.ContainsKey(key))
                    return Objects[key];

                var instance = initFunc != null
                    ? initFunc(dependencyContainer)
                    : DependencyActivator.GetInstance(key.Type, dependencyContainer, ctorTypes, arguments);
                Objects.Add(key, instance);
                return instance;                
            }
        }

        public void Destroy(DependencyKey key)
        {
            lock (Objects)
            {
                if (Objects.ContainsKey(key))
                {
                    Objects.Remove(key);
                    return;
                }
                throw new Exception("The instance have not been initialized");                
            }
        }

        public void SetInstance(DependencyKey key, object instance)
        {
            lock (Objects)
            {
                if (Objects.ContainsKey(key))
                    throw new Exception("The instance have to destroy");
                Objects.Add(key, instance);                
            }
        }
    }
}