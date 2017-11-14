using System;
using Solomonic.DependencyInjection.Lifecycles;

namespace Solomonic.DependencyInjection.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Struct)]
    public class DependencyAttribute : Attribute
    {
        private readonly ILifecycle _lifecycle;

        public DependencyAttribute(ILifecycle lifecycle)
        {
            _lifecycle = lifecycle;
        }

        public ILifecycle Lifecycle
        {
            get { return _lifecycle; }
        }
    }
}