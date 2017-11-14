using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Solomonic.DependencyInjection.Attributes;

namespace Solomonic.DependencyInjection
{
    public static class DependencyActivator
    {
        public static object GetInstance(Type type, DependencyContainer dependencyContainer, Type[] ctorTypes, IList<DependencyArgument> arguments)
        {
            var contructor = type.GetConstructor(ctorTypes ?? new Type[0]);

            if (contructor == null)
            {
                var contructors = type.GetConstructors();
                if (contructors.Length > 1)
                    throw new Exception("Not defined one of many constructors for the type");

                contructor = contructors[0];
            }

            var parameters = contructor.GetParameters();
            var paramInstances = new List<object>();
            var copyArguments = arguments!=null ? arguments.ToList() : new List<DependencyArgument>();
            foreach (var parameterInfo in parameters)
            {
                object paramInstance = null;
                
                foreach (var dependencyArgument in copyArguments)
                {
                    if (dependencyArgument.Type == parameterInfo.ParameterType)
                    {
                        paramInstance = dependencyArgument.Value;
                        copyArguments.Remove(dependencyArgument);
                        break;
                    }
                }

                paramInstance = paramInstance ?? dependencyContainer.GetInstance(parameterInfo.ParameterType);                
                paramInstances.Add(paramInstance);
            }

            var instance = Activator.CreateInstance(type, args: paramInstances.ToArray());
            var properties =
                type.GetProperties()
                    .Where(info => info.CanWrite && Attribute.IsDefined(info, typeof (DependencyPropertyAttribute)));
            foreach (var propertyInfo in properties)
            {
                var dependencyAttribute = propertyInfo.GetCustomAttribute(typeof(DependencyPropertyAttribute)) as DependencyPropertyAttribute;
                if (dependencyAttribute != null)
                {
                    var propertyInstance = dependencyContainer.GetInstance(propertyInfo.PropertyType, dependencyAttribute.Name);
                    propertyInfo.SetValue(instance, propertyInstance);
                }
            }
            
            return instance;
        }
    }
}
