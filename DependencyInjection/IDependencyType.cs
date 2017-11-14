using System;
using Solomonic.DependencyInjection.Lifecycles;

namespace Solomonic.DependencyInjection
{
    public interface IDependencyType<T>
    {
        IDependencyType<T> SetCtor<TArg>(Func<object> argument);
        IDependencyType<T> SetCtor<T1, T2>(Func<object> argument1, Func<object> argument2);
        IDependencyType<T> SetCtor<T1, T2, T3>(Func<object> argument1, Func<object> argument2, Func<object> argument3);
        IDependencyType<T> SetCtor<T1, T2, T3, T4>(Func<object> argument1, Func<object> argument2,
            Func<object> argument3, Func<object> argument4);
        IDependencyType<T> SetCtor<T1, T2, T3, T4, T5>(Func<object> argument1, Func<object> argument2,
            Func<object> argument3, Func<object> argument4, Func<object> argument5);
        IDependencyType<T> SetCtor<T1, T2, T3, T4, T5, T6>(Func<object> argument1, Func<object> argument2,
            Func<object> argument3, Func<object> argument4, Func<object> argument5, Func<object> argument6);
        IDependencyType<T> SetInitFunc(Func<DependencyContainer, object> initFunc);
        IDependencyType<T> SetCtor(params object[] arguments);
        IDependencyType<T> SetCtor<TArg>(TArg argument);
        IDependencyType<T> SetCtor<T1, T2>(T1 argument1, T2 argument2);
        IDependencyType<T> SetCtor<T1, T2, T3>(T1 argument1, T2 argument2, T3 argument3);
        IDependencyType<T> SetCtor<T1, T2, T3, T4>(T1 argument1, T2 argument2, T3 argument3, T4 argument4);
        IDependencyType<T> SetCtor<T1, T2, T3, T4, T5>(T1 argument1, T2 argument2, T3 argument3, T4 argument4,
            T5 argument5);
        IDependencyType<T> SetCtor<T1, T2, T3, T4, T5, T6>(T1 argument1, T2 argument2, T3 argument3, T4 argument4,
            T5 argument5, T6 argument6);

        IDependencyType<T> SetCtor(params Type[] types);
        IDependencyType<T> SetCtor<TArg>();
        IDependencyType<T> SetCtor<T1, T2>();
        IDependencyType<T> SetCtor<T1, T2, T3>();
        IDependencyType<T> SetCtor<T1, T2, T3, T4>();
        IDependencyType<T> SetCtor<T1, T2, T3, T4, T5>();
        IDependencyType<T> SetCtor<T1, T2, T3, T4, T5, T6>();
        IDependencyType<T> SetLifecycle(ILifecycle lifecycle);
    }

    internal interface IDependencyType
    {
        object GetInstance(DependencyContainer dependencyContainer);        
        void SetInstance(object instance);
        void Destroy(DependencyContainer dependencyContainer);
    }
}