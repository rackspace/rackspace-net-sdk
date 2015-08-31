using System;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using Flurl.Http;
using Rackspace.Testing;
using Xunit;

namespace Rackspace
{
    public class RackspaceNetTests : IDisposable
    {
        public RackspaceNetTests()
        {
            RackspaceNet.ResetDefaults();
        }

        public void Dispose()
        {
            RackspaceNet.ResetDefaults();
        }

        [Fact]
        public void ResetDefaults_ResetsFlurlConfiguration()
        {
            RackspaceNet.Configure();
            Assert.NotNull(FlurlHttp.Configuration.BeforeCall);
            RackspaceNet.ResetDefaults();
            Assert.Null(FlurlHttp.Configuration.BeforeCall);
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
        public async void UserAgentOnlyListedOnceTest()
        {
            using (var httpTest = new HttpTest())
            {
                RackspaceNet.Configure();
                RackspaceNet.Configure();

                await "http://api.com".GetAsync();

                var userAgent = httpTest.CallLog[0].Request.Headers.UserAgent.ToString();
                var matches = new Regex("rackspace").Matches(userAgent);
                Assert.Equal(1, matches.Count);
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
