using Solomonic.DependencyInjection.Lifecycles;

namespace Solomonic.DependencyInjection.Attributes
{
    public class ThreadLifecycleAttribute : DependencyAttribute
    {
        public ThreadLifecycleAttribute()
            : base(new ThreadLifecycle())
        {
        }
    }
}