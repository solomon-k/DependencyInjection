using Solomonic.DependencyInjection.Attributes;
using Solomonic.DependencyInjection.Lifecycles;
using Solomonic.DependencyInjection.MVC.Lifecycles;

namespace Solomonic.DependencyInjection.MVC.Attributes
{
    public class HttpContextLifecycleAttribute : DependencyAttribute
    {
        public HttpContextLifecycleAttribute() : base(new HttpContextLifecycle())
        {
        }
    }
}
