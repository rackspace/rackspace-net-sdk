using OpenStack.Authentication;

namespace Rackspace.Testing
{
    /// <summary>
    /// Use this instead of <see cref="Flurl.Http.Testing.HttpTest"/> for any OpenStack.NET unit tests.
    /// <para>
    /// This extends Flurl's default HttpTest to use <see cref="AuthenticatedMessageHandler"/> in unit tests. 
    /// If you use the default HttpTest, then any tests which rely upon authentication handling (e.g retrying a request when a token expires) will fail.
    /// </para>
    /// </summary>
    public class HttpTest : OpenStack.Testing.HttpTest
    {
    }
}