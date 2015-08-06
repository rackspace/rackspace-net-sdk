using System;
using System.Linq;
using System.Net;
using Rackspace.CloudNetworks.v2.Serialization;
using Rackspace.Synchronous;
using Rackspace.Testing;
using Xunit;

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
                Identifier networkId = Guid.NewGuid();
                httpTest.RespondWithJson(new NetworkCollection(new[] {new Network {Id = networkId}}));

                var networks = _cloudNetworkService.ListNetworks();

                httpTest.ShouldHaveCalled("*/networks");
                Assert.NotNull(networks);
                Assert.Equal(1, networks.Count());
                Assert.Equal(networkId, networks.First().Id);
            }
        }

        [Fact]
        public void CreateNetwork()
        {
            using (var httpTest = new HttpTest())
            {
                Identifier networkId = Guid.NewGuid();
                httpTest.RespondWithJson(new Network{Id = networkId});

                var definition = new NetworkDefinition();
                var network = _cloudNetworkService.CreateNetwork(definition);

                httpTest.ShouldHaveCalled("*/networks");
                Assert.NotNull(network);
                Assert.Equal(networkId, network.Id);
            }
        }
        
        [Fact]
        public void GetNetwork()
        {
            using (var httpTest = new HttpTest())
            {
                Identifier networkId = Guid.NewGuid();
                httpTest.RespondWithJson(new Network { Id = networkId });

                var network = _cloudNetworkService.GetNetwork(networkId);

                httpTest.ShouldHaveCalled("*/networks/" + networkId);
                Assert.NotNull(network);
                Assert.Equal(networkId, network.Id);
            }
        }

        [Fact]
        public void DeleteNetwork()
        {
            using (var httpTest = new HttpTest())
            {
                Identifier networkId = Guid.NewGuid();
                httpTest.RespondWith((int)HttpStatusCode.NoContent, "All gone!");

                _cloudNetworkService.DeleteNetwork(networkId);

                httpTest.ShouldHaveCalled("*/networks/" + networkId);
            }
        }

        [Fact]
        public void WhenDeleteNetwork_Returns404NotFound_ShouldConsiderRequestSuccessful()
        {
            using (var httpTest = new HttpTest())
            {
                Identifier networkId = Guid.NewGuid();
                httpTest.RespondWith((int)HttpStatusCode.NotFound, "Not here, boss...");

                _cloudNetworkService.DeleteNetwork(networkId);

                httpTest.ShouldHaveCalled("*/networks/" + networkId);
            }
        }

        [Fact]
        public void UpdateNetwork()
        {
            using (var httpTest = new HttpTest())
            {
                Identifier networkId = Guid.NewGuid();
                httpTest.RespondWithJson(new Network {Id = networkId});

                var definition = new NetworkDefinition { Name = "new network name" };
                var network = _cloudNetworkService.UpdateNetwork(networkId, definition);

                httpTest.ShouldHaveCalled("*/networks/" + networkId);
                Assert.NotNull(network);
                Assert.Equal(networkId, network.Id);
            }
        }
    }
}
