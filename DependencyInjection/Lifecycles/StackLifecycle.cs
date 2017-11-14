using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Solomonic.DependencyInjection.Lifecycles
{
    public class StackLifecycle : CustomLifecycle<MethodBase>
    {
        public StackLifecycle() : base(() =>
        {
            var stackFrames = new StackTrace().GetFrames();
            return stackFrames.First(x => x.GetMethod().Module != stackFrames[0].GetMethod().Module).GetMethod();
        })
        {
        }
    }   
}
