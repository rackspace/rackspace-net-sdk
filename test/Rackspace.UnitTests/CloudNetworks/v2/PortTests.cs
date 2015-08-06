using System;
using System.Linq;
using System.Net;
using Rackspace.CloudNetworks.v2.Serialization;
using Rackspace.Synchronous;
using Rackspace.Testing;
using Xunit;

namespace Rackspace.CloudNetworks.v2
{
    public class PortTests
    {
        private readonly CloudNetworkService _cloudNetworkService;

        public PortTests()
        {
            _cloudNetworkService = new CloudNetworkService(Stubs.IdentityService, "region");
        }

        [Fact]
        public void ListPorts()
        {
            using (var httpTest = new HttpTest())
            {
                Identifier portId = Guid.NewGuid();
                httpTest.RespondWithJson(new PortCollection(new[] {new Port {Id = portId}}));

                var ports = _cloudNetworkService.ListPorts();

                httpTest.ShouldHaveCalled("*/ports");
                Assert.NotNull(ports);
                Assert.Equal(1, ports.Count());
                Assert.Equal(portId, ports.First().Id);
            }
        }

        [Fact]
        public void CreatePort()
        {
            using (var httpTest = new HttpTest())
            {
                Identifier networkId = Guid.NewGuid();
                Identifier portId = Guid.NewGuid();
                httpTest.RespondWithJson(new Port { Id = portId });

                var definition = new PortCreateDefinition(networkId);
                var port = _cloudNetworkService.CreatePort(definition);

                httpTest.ShouldHaveCalled("*/ports");
                Assert.NotNull(port);
                Assert.Equal(portId, port.Id);
            }
        }
        
        [Fact]
        public void GetPorts()
        {
            using (var httpTest = new HttpTest())
            {
                Identifier portId = Guid.NewGuid();
                httpTest.RespondWithJson(new Port { Id = portId });

                var port = _cloudNetworkService.GetPort(portId);

                httpTest.ShouldHaveCalled("*/ports/" + portId);
                Assert.NotNull(port);
                Assert.Equal(portId, port.Id);
            }
        }

        [Fact]
        public void DeletePort()
        {
            using (var httpTest = new HttpTest())
            {
                Identifier portId = Guid.NewGuid();
                httpTest.RespondWith((int)HttpStatusCode.NoContent, "All gone!");

                _cloudNetworkService.DeletePort(portId);

                httpTest.ShouldHaveCalled("*/ports/" + portId);
            }
        }

        [Fact]
        public void WhenDeletePort_Returns404NotFound_ShouldConsiderRequestSuccessful()
        {
            using (var httpTest = new HttpTest())
            {
                Identifier portId = Guid.NewGuid();
                httpTest.RespondWith((int)HttpStatusCode.NotFound, "Not here, boss...");

                _cloudNetworkService.DeletePort(portId);

                httpTest.ShouldHaveCalled("*/ports/" + portId);
            }
        }

        [Fact]
        public void UpdatePort()
        {
            using (var httpTest = new HttpTest())
            {
                Identifier portId = Guid.NewGuid();
                httpTest.RespondWithJson(new Port { Id = portId });

                var definition = new PortUpdateDefinition { Name = "new subnet name" };
                var port = _cloudNetworkService.UpdatePort(portId, definition);

                httpTest.ShouldHaveCalled("*/ports/" + portId);
                Assert.NotNull(port);
                Assert.Equal(portId, port.Id);
            }
        }
    }
}
