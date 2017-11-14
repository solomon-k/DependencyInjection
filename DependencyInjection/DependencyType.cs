using System;
using System.Collections.Generic;
using System.Linq;
using Solomonic.DependencyInjection.Lifecycles;

namespace Solomonic.DependencyInjection
{
    public class DependencyType<T> : IDependencyType, IDependencyType<T>
    {
        #region variables
        private Type[] _ctorTypes;
        private Func<DependencyContainer, object> _initFunc;
        private readonly IList<DependencyArgument> _arguments = new List<DependencyArgument>();
        private ILifecycle _lifecycle = new UniqueLifecycle();
        private readonly DependencyKey _key;
        #endregion

        #region internal
        internal DependencyType(DependencyKey key)
        {
            _key = key;
        }
        public object GetInstance(DependencyContainer dependencyContainer)
        {
            return _lifecycle.GetInstance(_key, dependencyContainer, _ctorTypes, _arguments, _initFunc);
        }
        public void SetInstance(object instance)
        {
            _lifecycle.SetInstance(_key, instance);
        }
        public void Destroy(DependencyContainer dependencyContainer)
        {
            _lifecycle.Destroy(_key);
        }
        #endregion

        #region public
        public IDependencyType<T> SetInitFunc(Func<DependencyContainer, object> initFunc)
        {
            _initFunc = initFunc;
            return this;
        }
        public IDependencyType<T> SetCtor(params object[] arguments)
        {
            return SetCtor(arguments.Select(x => new DependencyArgument(x.GetType(), x)).ToArray());
        }
        public IDependencyType<T> SetCtor(params Type[] types)
        {
            _ctorTypes = types;
            return this;
        }
        public IDependencyType<T> SetLifecycle(ILifecycle lifecycle)
        {
            _lifecycle = lifecycle;
            return this;
        }


        #region SetCtor<T1, ...>(Func<object> argument...)

        public IDependencyType<T> SetCtor<TArg>(Func<object> argument)
        {
            return SetCtor(new DependencyArgument(typeof(TArg), getter: argument));
        }
        public IDependencyType<T> SetCtor<T1, T2>(Func<object> argument1, Func<object> argument2)
        {
            return SetCtor(new DependencyArgument(typeof(T1), argument1),
                new DependencyArgument(typeof(T2), argument2));
        }
        public IDependencyType<T> SetCtor<T1, T2, T3>(Func<object> argument1, Func<object> argument2, Func<object> argument3)
        {
            return SetCtor(new DependencyArgument(typeof(T1), argument1),
                new DependencyArgument(typeof(T2), argument2),
                new DependencyArgument(typeof(T3), argument3));
        }
        public IDependencyType<T> SetCtor<T1, T2, T3, T4>(Func<object> argument1, Func<object> argument2, Func<object> argument3, Func<object> argument4)
        {
            return SetCtor(new DependencyArgument(typeof(T1), argument1),
                new DependencyArgument(typeof(T2), argument2),
                new DependencyArgument(typeof(T3), argument3),
                new DependencyArgument(typeof(T4), argument4));
        }
        public IDependencyType<T> SetCtor<T1, T2, T3, T4, T5>(Func<object> argument1, Func<object> argument2, Func<object> argument3, Func<object> argument4, Func<object> argument5)
        {
            return SetCtor(new DependencyArgument(typeof(T1), argument1),
                new DependencyArgument(typeof(T2), argument2),
                new DependencyArgument(typeof(T3), argument3),
                new DependencyArgument(typeof(T4), argument4),
                new DependencyArgument(typeof(T5), argument5));
        }
        public IDependencyType<T> SetCtor<T1, T2, T3, T4, T5, T6>(Func<object> argument1, Func<object> argument2, Func<object> argument3, Func<object> argument4, Func<object> argument5, Func<object> argument6)
        {
            return SetCtor(new DependencyArgument(typeof(T1), argument1),
                new DependencyArgument(typeof(T2), argument2),
                new DependencyArgument(typeof(T3), argument3),
                new DependencyArgument(typeof(T4), argument4),
                new DependencyArgument(typeof(T5), argument5),
                new DependencyArgument(typeof(T6), argument6));
        }
        #endregion
        #region SetCtor<T1, ...> (arguments)

        private IDependencyType<T> SetCtor(params DependencyArgument[] arguments)
        {
            foreach (var argument in arguments)
            {
                _arguments.Add(argument);
            }
            return this;
        }

        public IDependencyType<T> SetCtor<TArg>(TArg argument)
        {
            return SetCtor(new DependencyArgument(typeof(TArg), argument));
        }
        public IDependencyType<T> SetCtor<T1, T2>(T1 argument1, T2 argument2)
        {
            return SetCtor(new DependencyArgument(typeof(T1), argument1),
                new DependencyArgument(typeof(T2), argument2));
        }
        public IDependencyType<T> SetCtor<T1, T2, T3>(T1 argument1, T2 argument2, T3 argument3)
        {
            return SetCtor(new DependencyArgument(typeof(T1), argument1),
                new DependencyArgument(typeof(T2), argument2),
                new DependencyArgument(typeof(T3), argument3));
        }
        public IDependencyType<T> SetCtor<T1, T2, T3, T4>(T1 argument1, T2 argument2, T3 argument3, T4 argument4)
        {
            return SetCtor(new DependencyArgument(typeof(T1), argument1),
                new DependencyArgument(typeof(T2), argument2),
                new DependencyArgument(typeof(T3), argument3),
                new DependencyArgument(typeof(T4), argument4));
        }
        public IDependencyType<T> SetCtor<T1, T2, T3, T4, T5>(T1 argument1, T2 argument2, T3 argument3, T4 argument4, T5 argument5)
        {
            return SetCtor(new DependencyArgument(typeof(T1), argument1),
                new DependencyArgument(typeof(T2), argument2),
                new DependencyArgument(typeof(T3), argument3),
                new DependencyArgument(typeof(T4), argument4),
                new DependencyArgument(typeof(T5), argument5));
        }
        public IDependencyType<T> SetCtor<T1, T2, T3, T4, T5, T6>(T1 argument1, T2 argument2, T3 argument3, T4 argument4, T5 argument5, T6 argument6)
        {
            return SetCtor(new DependencyArgument(typeof(T1), argument1),
                new DependencyArgument(typeof(T2), argument2),
                new DependencyArgument(typeof(T3), argument3),
                new DependencyArgument(typeof(T4), argument4),
                new DependencyArgument(typeof(T5), argument5),
                new DependencyArgument(typeof(T6), argument6));
        }
        #endregion
        #region SetCtor<T1,T2,...>()
        public IDependencyType<T> SetCtor<TArg>()
        {
            return SetCtor(typeof(TArg));
        }
        public IDependencyType<T> SetCtor<T1, T2>()
        {
            return SetCtor(typeof(T1), typeof(T2));
        }
        public IDependencyType<T> SetCtor<T1, T2, T3>()
        {
            return SetCtor(typeof(T1), typeof(T2), typeof(T3));
        }
        public IDependencyType<T> SetCtor<T1, T2, T3, T4>()
        {
            return SetCtor(typeof(T1), typeof(T2), typeof(T3), typeof(T4));
        }
        public IDependencyType<T> SetCtor<T1, T2, T3, T4, T5>()
        {
            return SetCtor(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5));
        }
        public IDependencyType<T> SetCtor<T1, T2, T3, T4, T5, T6>()
        {
            return SetCtor(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6));
        }
        #endregion

        #endregion
    }
}