using Solomonic.DependencyInjection.Attributes;
using Solomonic.DependencyInjection.Lifecycles;
using Solomonic.DependencyInjection.MVC.Lifecycles;

namespace Solomonic.DependencyInjection.MVC.Attributes
{
    public class HttpSessionLifecycleAttribute : DependencyAttribute
    {
        public HttpSessionLifecycleAttribute() : base(new HttpSessionLifecycle())
        {
        }
    }
}
