using System;
using System.Net.Http.Headers;
using Flurl.Http;
using Rackspace.Testing;
using Xunit;

namespace Rackspace
{
    public class RackspaceNetTests : IDisposable
    {
        public void Dispose()
        {
            RackspaceNet.Configuration.ResetDefaults();
        }

        [Fact]
        public async void UserAgentTest()
        {
            using (var httpTest = new HttpTest())
            {
                RackspaceNet.Configure();

                await "http://api.com".GetAsync();

                var userAgent = httpTest.CallLog[0].Request.Headers.UserAgent.ToString();
                Assert.Contains("rackspace.net", userAgent);
                Assert.Contains("openstack.net", userAgent);
            }
        }

        [Fact]
        public async void UserAgentWithApplicationSuffixTest()
        {
            using (var httpTest = new HttpTest())
            {
                RackspaceNet.Configure(configure: options => options.UserAgents.Add(new ProductInfoHeaderValue("(unit-tests)")));

                await "http://api.com".GetAsync();

                var userAgent = httpTest.CallLog[0].Request.Headers.UserAgent.ToString();
                Assert.Contains("rackspace.net", userAgent);
                Assert.Contains("unit-tests", userAgent);
            }
        }
    }
}
