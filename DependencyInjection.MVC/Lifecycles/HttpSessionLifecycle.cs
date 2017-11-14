using System.Web;
using Solomonic.DependencyInjection.Lifecycles;

namespace Solomonic.DependencyInjection.MVC.Lifecycles
{
    public class HttpSessionLifecycle : CustomLifecycle<string>
    {
        public HttpSessionLifecycle()
            : base(() => HttpContext.Current.Session.SessionID)
        {
        }
    }    
}