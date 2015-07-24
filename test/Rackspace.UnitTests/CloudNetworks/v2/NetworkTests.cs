using System.Linq;
using System.Net;
using Rackspace.Synchronous;
using Rackspace.Testing;
using Xunit;
using Xunit.Abstractions;

namespace Rackspace.CloudNetworks.v2
{
    public class NetworkTests
    {
        private readonly CloudNetworkService _cloudNetworkService;

        public NetworkTests()
        {
            _cloudNetworkService = new CloudNetworkService(Stubs.IdentityService, "region");
        }

        [Fact]
        public void ListNetworks()
        {
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWithJson(new NetworkCollection(new[] {new Network {Id = "network-id"}}));

                var networks = _cloudNetworkService.ListNetworks();

                httpTest.ShouldHaveCalled("*/networks");
                Assert.NotNull(networks);
                Assert.Equal(1, networks.Count());
                Assert.Equal("network-id", networks.First().Id);
            }
        }

        [Fact]
        public void CreateNetwork()
        {
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWithJson(new Network{Id = "network-id"});

                var definition = new NetworkDefinition();
                var network = _cloudNetworkService.CreateNetwork(definition);

                httpTest.ShouldHaveCalled("*/networks");
                Assert.NotNull(network);
                Assert.Equal("network-id", network.Id);
            }
        }
        
        [Fact]
        public void GetNetwork()
        {
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWithJson(new Network { Id = "network-id" });

                var network = _cloudNetworkService.GetNetwork("network-id");

                httpTest.ShouldHaveCalled("*/networks/network-id");
                Assert.NotNull(network);
                Assert.Equal("network-id", network.Id);
            }
        }

        [Fact]
        public void DeleteNetwork()
        {
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith((int)HttpStatusCode.NoContent, "All gone!");

                _cloudNetworkService.DeleteNetwork("network-id");

                httpTest.ShouldHaveCalled("*/networks/network-id");
            }
        }

        [Fact]
        public void WhenDeleteNetwork_Returns404NotFound_ShouldConsiderRequestSuccessful()
        {
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith((int)HttpStatusCode.NotFound, "Not here, boss...");

                _cloudNetworkService.DeleteNetwork("network-id");

                httpTest.ShouldHaveCalled("*/networks/network-id");
            }
        }
    }
}
