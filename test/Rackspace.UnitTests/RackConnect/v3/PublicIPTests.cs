using System;
using System.Linq;
using Rackspace.Synchronous;
using Rackspace.Testing;
using Xunit;

namespace Rackspace.RackConnect.v3
{
    public class PublicIPTests
    {
        private readonly RackConnectService _rackConnectService;

        public PublicIPTests()
        {
            _rackConnectService = new RackConnectService(Stubs.IdentityService, "region");
        }

        [Fact]
        public void ListPublicIPs()
        {
            using (var httpTest = new HttpTest())
            {
                Identifier id = Guid.NewGuid();
                httpTest.RespondWithJson(new[] {new PublicIP {Id = id}});

                var results = _rackConnectService.ListPublicIPs();

                Assert.NotNull(results);
                Assert.Equal(1, results.Count());
                Assert.Equal(id, results.First().Id);
            }
        }

        [Fact]
        public void AssignPublicIP()
        {
            using (var httpTest = new HttpTest())
            {
                string serverId = Guid.NewGuid().ToString();
                Identifier id = Guid.NewGuid();
                httpTest.RespondWithJson(new PublicIP { Id = id });

                var result = _rackConnectService.AssignPublicIP(serverId);

                Assert.NotNull(result);
                Assert.Equal(id, result.Id);
            }
        }

        [Fact]
        public void WaitUntilActive()
        {
            using (var httpTest = new HttpTest())
            {
                string serverId = Guid.NewGuid().ToString();
                Identifier id = Guid.NewGuid();
                httpTest.RespondWithJson(new PublicIP {Id = id, Status = PublicIPStatus.Adding});
                httpTest.RespondWithJson(new PublicIP {Id = id, Status = PublicIPStatus.Active, PublicIPv4Address = "10.0.0.1"});

                var ip = _rackConnectService.AssignPublicIP(serverId);
                ip.WaitUntilActive();

                Assert.NotNull(ip);
                Assert.Equal(id, ip.Id);
                Assert.NotNull(ip.PublicIPv4Address);
            }
        }

        [Fact]
        public void WaitUntilActive_ThrowsException_WhenCalledOnManuallyCreatedInstance()
        {
            var ip = new PublicIP();

            Assert.Throws<InvalidOperationException>(() => ip.WaitUntilActive());
        }

        [Fact]
        public void ListNetworks()
        {
            using (var httpTest = new HttpTest())
            {
                Identifier id = Guid.NewGuid();
                httpTest.RespondWithJson(new[] { new NetworkReference() { Id = id } });

                var results = _rackConnectService.ListNetworks();

                Assert.NotNull(results);
                Assert.Equal(1, results.Count());
                Assert.Equal(id, results.First().Id);
            }
        }
    }
}
