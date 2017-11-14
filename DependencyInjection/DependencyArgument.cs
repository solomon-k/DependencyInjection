using System;

namespace Solomonic.DependencyInjection
{
    public struct DependencyArgument
    {
        private readonly Type _type;
        private readonly Func<object> _getter;
        
        public DependencyArgument(Type type, object value)
        {
            _type = type;
            _getter = () => value;
        }
        public DependencyArgument(Type type, Func<object> getter)
        {
            _type = type;
            _getter = getter;
        }

        public object Value
        {
            get { return _getter(); }
        }

        public Type Type
        {
            get { return _type; }
        }
    }
}