using System;
using System.Linq;
using System.Net;
using Newtonsoft.Json.Linq;
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

                httpTest.ShouldHaveCalled($"*/public_ips");
                Assert.NotNull(results);
                Assert.Equal(1, results.Count());
                Assert.Equal(id, results.First().Id);
                Assert.All(results.OfType<IServiceResource<RackConnectService>>(), ip => Assert.NotNull(ip.Owner));
            }
        }

        [Fact]
        public void ListPublicIPsForServer()
        {
            using (var httpTest = new HttpTest())
            {
                string serverId = Guid.NewGuid().ToString();
                Identifier id = Guid.NewGuid();
                httpTest.RespondWithJson(new[] {new PublicIP {Id = id}});

                var results = _rackConnectService.ListPublicIPs(new ListPublicIPsFilter {ServerId = serverId});

                httpTest.ShouldHaveCalled($"*/public_ips?cloud_server_id={serverId}");
                Assert.NotNull(results);
                Assert.Equal(1, results.Count());
                Assert.Equal(id, results.First().Id);
            }
        }

        [Fact]
        public void ListPersistentPublicIPs()
        {
            using (var httpTest = new HttpTest())
            {
                Identifier id = Guid.NewGuid();
                httpTest.RespondWithJson(new[] { new PublicIP { Id = id } });

                var results = _rackConnectService.ListPublicIPs(new ListPublicIPsFilter { IsRetained = true });

                httpTest.ShouldHaveCalled($"*/public_ips?retain=True");
                Assert.NotNull(results);
                Assert.Equal(1, results.Count());
                Assert.Equal(id, results.First().Id);
            }
        }

        [Fact]
        public void GetPublicIP()
        {
            using (var httpTest = new HttpTest())
            {
                Identifier id = Guid.NewGuid();
                httpTest.RespondWithJson(new PublicIP { Id = id });

                var result = _rackConnectService.GetPublicIP(id);

                httpTest.ShouldHaveCalled($"*/public_ips/{id}");
                Assert.NotNull(result);
                Assert.Equal(id, result.Id);
                Assert.NotNull(((IServiceResource<RackConnectService>)result).Owner);
            }
        }

        [Fact]
        public void ProvisionPublicIP()
        {
            using (var httpTest = new HttpTest())
            {
                string serverId = Guid.NewGuid().ToString();
                Identifier id = Guid.NewGuid();
                httpTest.RespondWithJson(new PublicIP { Id = id });

                var ipRequest = new PublicIPDefinition {ServerId = serverId};
                var result = _rackConnectService.ProvisionPublicIP(ipRequest);

                httpTest.ShouldHaveCalled($"*/public_ips");
                Assert.NotNull(result);
                Assert.Equal(id, result.Id);
                Assert.NotNull(((IServiceResource<RackConnectService>)result).Owner);
            }
        }

        [Fact]
        public void WaitUntilActive()
        {
            using (var httpTest = new HttpTest())
            {
                Identifier id = Guid.NewGuid();
                httpTest.RespondWithJson(new PublicIP {Id = id, Status = PublicIPStatus.Adding});
                httpTest.RespondWithJson(new PublicIP {Id = id, Status = PublicIPStatus.Active, PublicIPv4Address = "10.0.0.1"});

                var ip = _rackConnectService.GetPublicIP(id);
                ip.WaitUntilActive();

                Assert.NotNull(ip);
                Assert.Equal(id, ip.Id);
                Assert.NotNull(ip.PublicIPv4Address);
            }
        }

        [Fact]
        public void WaitUntilActive_ThrowsException_WhenAddFails()
        {
            using (var httpTest = new HttpTest())
            {
                Identifier id = Guid.NewGuid();
                httpTest.RespondWithJson(new PublicIP { Id = id, Status = PublicIPStatus.Adding });
                httpTest.RespondWithJson(new PublicIP { Id = id, Status = PublicIPStatus.AddFailed, StatusDetails = "No IP for you!"});

                var ip = _rackConnectService.GetPublicIP(id);
                Assert.Throws<ServiceOperationFailedException>(() => ip.WaitUntilActive());
            }
        }

        [Fact]
        public void WaitUntilActive_ThrowsException_WhenCalledOnManuallyCreatedInstance()
        {
            var ip = new PublicIP();

            Assert.Throws<InvalidOperationException>(() => ip.WaitUntilActive());
        }

        [Fact]
        public void RemovePublicIP()
        {
            using (var httpTest = new HttpTest())
            {
                Identifier id = Guid.NewGuid();
                httpTest.RespondWithJson(new PublicIP { Id = id });

                _rackConnectService.RemovePublicIP(id);

                httpTest.ShouldHaveCalled($"*/public_ips/{id}");
            }
        }

        [Fact]
        public void WaitUntilRemoved()
        {
            using (var httpTest = new HttpTest())
            {
                Identifier id = Guid.NewGuid();
                httpTest.RespondWithJson(new PublicIP { Id = id, Status = PublicIPStatus.Active });
                httpTest.RespondWith((int) HttpStatusCode.NoContent, "All gone!");
                httpTest.RespondWithJson(new PublicIP { Id = id, Status = PublicIPStatus.Removed });

                var ip = _rackConnectService.GetPublicIP(id);
                ip.Remove();
                ip.WaitUntilRemoved();
            }
        }

        [Fact]
        public void WaitUntilRemoved_ThrowsException_WhenRemoveFails()
        {
            using (var httpTest = new HttpTest())
            {
                Identifier id = Guid.NewGuid();
                httpTest.RespondWithJson(new PublicIP { Id = id, Status = PublicIPStatus.Active });
                httpTest.RespondWithJson(new PublicIP { Id = id, Status = PublicIPStatus.Active });
                httpTest.RespondWith(JObject.Parse(@"{'status':'REMOVE_FAILED'}").ToString());

                var ip = _rackConnectService.GetPublicIP(id);
                ip.Remove();
                Assert.Throws<ServiceOperationFailedException>(() =>ip.WaitUntilRemoved());
            }
        }

        [Fact]
        public void WaitUntilRemoved_AcceptsNotFoundExceptionAsSuccess()
        {
            using (var httpTest = new HttpTest())
            {
                Identifier id = Guid.NewGuid();
                httpTest.RespondWithJson(new PublicIP { Id = id, Status = PublicIPStatus.Active });
                httpTest.RespondWith((int) HttpStatusCode.NoContent, "All gone!");
                httpTest.RespondWith((int) HttpStatusCode.NotFound, "Not here, boss!");

                var ip = _rackConnectService.GetPublicIP(id);
                ip.Remove();
                ip.WaitUntilRemoved();
            }
        }

        [Fact]
        public void WaitUntilRemoved_ThrowsException_WhenCalledOnManuallyCreatedInstance()
        {
            var ip = new PublicIP();

            Assert.Throws<InvalidOperationException>(() => ip.WaitUntilRemoved());
        }
    }
}
