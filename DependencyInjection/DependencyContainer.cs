using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Solomonic.DependencyInjection.Attributes;
using Solomonic.DependencyInjection.Lifecycles;

namespace Solomonic.DependencyInjection
{
    public class DependencyContainer
    {
        private readonly IDictionary<DependencyKey, IDependencyType> _typeDependencies =
            new ConcurrentDictionary<DependencyKey, IDependencyType>();

        private readonly Guid _guid = Guid.NewGuid();
        public Guid Guid { get { return _guid; } }
        public DependencyContainer(Action<DependencyContainer> action)
        {
            action(this);
        }

        #region Registry
        public DependencyType<T1> Registry<T1, T2>(string name = null)
        {
            lock (_typeDependencies)
            {
                var type1 = typeof(T1);
                var type2 = typeof(T2);
                var key = new DependencyKey(_guid, name, type1);
                if (_typeDependencies.ContainsKey(key))
                {
                    throw new Exception("Type dependence already exist");
                }

                var typeDependency = new DependencyType<T1>(new DependencyKey(_guid, name, type2));
                _typeDependencies.Add(key, typeDependency);
                return typeDependency;
            }
        }
        public DependencyType<T1> For<T1, T2>(string name = null)
        {
            lock (_typeDependencies)
            {
                var type1 = typeof(T1);
                var type2 = typeof(T2);
                var key = new DependencyKey(_guid, name, type1);
                if (_typeDependencies.ContainsKey(key))
                {
                    return (DependencyType<T1>)_typeDependencies[key];
                }

                var typeDependency = new DependencyType<T1>(new DependencyKey(_guid, name, type2));
                _typeDependencies.Add(key, typeDependency);
                return typeDependency;
            }
        }
        public void UnRegistry<T>(string name = null)
        {
            lock (_typeDependencies)
            {
                var type = typeof(T);
                var key = new DependencyKey(_guid, name, type);
                if (!_typeDependencies.ContainsKey(key))
                {
                    throw new Exception("Type dependence not registrated");
                }

                _typeDependencies.Remove(key);
            }
        }
        public bool IsRegistry<T>(string name = null)
        {
            lock (_typeDependencies)
            {
                var type = typeof(T);
                var key = new DependencyKey(_guid, name, type);
                return _typeDependencies.ContainsKey(key);
            }
        }
        #endregion
        
        #region GetInstance
        public object GetInstance(Type type, string name = null)
        {
            lock (_typeDependencies)
            {
                if (!_typeDependencies.ContainsKey(new DependencyKey(_guid, name, type)))
                {
                    if(type.IsClass && !type.IsAbstract)
                    {
                        return CreateInstance(type);
                    } else
                        throw new Exception("The type not registrated");
                }

                return _typeDependencies[new DependencyKey(_guid, name, type)].GetInstance(this);
            }
        }
        public T GetInstance<T>(string name = null)
        {
            var type = typeof(T);
            var instance = GetInstance(type, name);
            if (instance != null)
                return (T)instance;
            return default(T);
        }
        public T CreateInstance<T>()
        {
            var instance = CreateInstance(typeof(T));
            return instance != null ? (T)instance : default(T);
        }
        public object CreateInstance(Type type)
        {
            var uniqueLifecycle = new UniqueLifecycle();
            return uniqueLifecycle.GetInstance(new DependencyKey(this.Guid, null, type), this, null, null, null);
        }
        #endregion

        #region SetInstance
        public void SetInstance(Type type, object instance, string name = null)
        {
            lock (_typeDependencies)
            {
                if (!_typeDependencies.ContainsKey(new DependencyKey(_guid, name, type)))
                {
                    throw new Exception("The type not registrated");
                }

                _typeDependencies[new DependencyKey(_guid, name, type)].SetInstance(instance);
            }
        }
        public void SetInstance<T>(T instance, string name = null)
        {
            var type = typeof(T);
            SetInstance(type, instance, name);
        }
        #endregion

        #region Destroy
        public void Destroy(Type type, string name = null)
        {
            lock (_typeDependencies)
            {
                if (!_typeDependencies.ContainsKey(new DependencyKey(_guid, name, type)))
                {
                    throw new Exception("The type not registrated");
                }

                _typeDependencies[new DependencyKey(_guid, name, type)].Destroy(this);
            }
        }
        public void Destroy<T>(string name = null)
        {
            var type = typeof(T);
            Destroy(type, name);
        }
        #endregion

        #region IsInitialized
        public bool IsInitialized(Type type, string name = null)
        {
            lock (_typeDependencies)
            {
                return _typeDependencies.ContainsKey(new DependencyKey(_guid, name, type));
            }            
        }
        public bool IsInitialized<T>(string name = null)
        {
            var type = typeof (T);
            return IsInitialized(type, name);
        }
        #endregion

        #region Auto

        public void AutoRegistryAssembly(Assembly assembly, AutoRegistryFlag flag = AutoRegistryFlag.Interfaces | AutoRegistryFlag.AbstractClasses)
        {
            var types = assembly.GetTypes();

            //registry Interfaces
            if (flag.HasFlag(AutoRegistryFlag.Interfaces))
            {
                foreach (
                    var interfaceType in
                        types.Where(
                            x =>
                                x.IsInterface && x.Name.StartsWith("I") &&
                                x.GetCustomAttribute<DependencyIgnoreAttribute>() == null))
                {
                    var classTypes = types.Where(x => x.Name == interfaceType.Name.Substring(1)).ToArray();
                    if (classTypes.Length == 0)
                        classTypes = types.Where(x => x.IsAssignableFrom(interfaceType) && x.IsClass && !x.IsAbstract).ToArray();
                    var classType = classTypes.Length == 1 ? classTypes.Single() : null;
                    if (classType != null && !classType.IsGenericType)
                    {
                        var typeDependency =
                            GetType().GetMethod("Registry")
                                .MakeGenericMethod(interfaceType, classType)
                                .Invoke(this, new object[] {null});

                        GetType().GetMethod("AutoSetLifecycle", BindingFlags.NonPublic | BindingFlags.Instance)
                            .MakeGenericMethod(interfaceType)
                            .Invoke(this, new object[] {typeDependency});
                    }
                }
            }
            
            //registry BaseClasses
            if (flag.HasFlag(AutoRegistryFlag.AbstractClasses))
            {
                foreach (
                    var abstractType in
                        types.Where(
                            x =>
                                x.IsAbstract && x.Name.StartsWith("Base") &&
                                x.GetCustomAttribute<DependencyIgnoreAttribute>() == null))
                {
                    var classType = types.FirstOrDefault(x => x.Name == abstractType.Name.Substring(4));
                    if (classType != null)
                    {                        
                        var typeDependency =
                            GetType().GetMethod("Registry")
                                .MakeGenericMethod(abstractType, classType)
                                .Invoke(this, new object[] { null });

                        GetType().GetMethod("AutoSetLifecycle", BindingFlags.NonPublic | BindingFlags.Instance)
                            .MakeGenericMethod(abstractType)
                            .Invoke(this, new object[] { typeDependency });                    
                    }
                }

            }
        }

        private void AutoSetLifecycle<T>(IDependencyType<T> typeDependency)
        {
            var dependencyAttribute =
                typeof(T).GetCustomAttribute<DependencyAttribute>();

            if (dependencyAttribute != null)
            {
                typeDependency.SetLifecycle(dependencyAttribute.Lifecycle);
            }
        }        
        #endregion
        
    }
}
