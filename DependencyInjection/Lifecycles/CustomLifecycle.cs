using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Solomonic.DependencyInjection.Lifecycles
{
    public class CustomLifecycle<TKey> : ILifecycle
    {
        private readonly Func<TKey> _gettingKeyFunc;
        private static readonly IDictionary<TKey, IDictionary<DependencyKey, object>> LifecycleObjects = new ConcurrentDictionary<TKey, IDictionary<DependencyKey, object>>();

        public CustomLifecycle(Func<TKey> gettingKeyFunc)
        {
            _gettingKeyFunc = gettingKeyFunc;
        }

        public object GetInstance(DependencyKey key, DependencyContainer dependencyContainer, Type[] ctorTypes, IList<DependencyArgument> arguments, Func<DependencyContainer, object> initFunc)
        {
            lock (LifecycleObjects)
            {
                var lifecycleKey = _gettingKeyFunc();
                
                IDictionary<DependencyKey, object> objects;
                if (!LifecycleObjects.ContainsKey(lifecycleKey))
                {
                    objects = new ConcurrentDictionary<DependencyKey, object>();
                    LifecycleObjects.Add(lifecycleKey, objects);
                }
                else
                {
                    objects = LifecycleObjects[lifecycleKey];
                }

                if (objects.ContainsKey(key))
                    return objects[key];

                var instance = initFunc != null
                    ? initFunc(dependencyContainer)
                    : DependencyActivator.GetInstance(key.Type, dependencyContainer, ctorTypes, arguments);

                objects.Add(key, instance);
                return instance;
            }
        }

        public void Destroy(DependencyKey dependencyKey)
        {
            lock (LifecycleObjects)
            {
                var lifecycleKey = _gettingKeyFunc();

                if (LifecycleObjects.ContainsKey(lifecycleKey))
                {
                    var objects = LifecycleObjects[lifecycleKey];
                    if (objects.ContainsKey(dependencyKey))
                    {
                        objects.Remove(dependencyKey);
                        return;
                    }
                }

                throw new Exception("The instance have not been initialized");
            }
        }

        public void SetInstance(DependencyKey key, object instance)
        {
            lock (LifecycleObjects)
            {
                var lifecycleKey = _gettingKeyFunc();
                IDictionary<DependencyKey, object> objects;
                if (!LifecycleObjects.ContainsKey(lifecycleKey))
                {
                    objects = new ConcurrentDictionary<DependencyKey, object>();
                    LifecycleObjects.Add(lifecycleKey, objects);
                }
                else
                {
                    objects = LifecycleObjects[lifecycleKey];
                }
                if (objects.ContainsKey(key))
                    throw new Exception("The instance have to destroy");
                objects.Add(key, instance);
            }
        }
    }
}