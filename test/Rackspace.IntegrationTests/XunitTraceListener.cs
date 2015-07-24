using Xunit.Abstractions;

namespace Rackspace
{
    public class XunitTraceListener : OpenStack.XunitTraceListener
    {
        public XunitTraceListener(ITestOutputHelper testLog) : base(testLog)
        {
        }
    }
}