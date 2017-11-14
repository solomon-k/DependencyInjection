using Solomonic.DependencyInjection.Lifecycles;

namespace Solomonic.DependencyInjection.Attributes
{
    public class StackLifecycleAttribute : DependencyAttribute
    {
        public StackLifecycleAttribute()
            : base(new StackLifecycle())
        {
        }
    }
}