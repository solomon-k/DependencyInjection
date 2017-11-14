using Solomonic.DependencyInjection.Lifecycles;

namespace Solomonic.DependencyInjection.Attributes
{
    public class SingletonLifecycleAttribute : DependencyAttribute
    {
        public SingletonLifecycleAttribute() : base(new SingletonLifecycle())
        {
        }
    }
}