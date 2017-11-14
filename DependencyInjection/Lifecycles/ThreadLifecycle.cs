using System.Threading;

namespace Solomonic.DependencyInjection.Lifecycles
{
    public class ThreadLifecycle : CustomLifecycle<int>
    {
        public ThreadLifecycle()
            : base(() => Thread.CurrentThread.ManagedThreadId)
        {
        }
    }
}