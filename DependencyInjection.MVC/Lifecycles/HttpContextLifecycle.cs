using System.Web;
using Solomonic.DependencyInjection.Lifecycles;

namespace Solomonic.DependencyInjection.MVC.Lifecycles
{
    public class HttpContextLifecycle : CustomLifecycle<HttpContext> {
        public HttpContextLifecycle()
            : base(() => HttpContext.Current)
        {
        }
    }
}
