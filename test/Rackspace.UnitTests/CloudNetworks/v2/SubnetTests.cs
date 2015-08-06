using System;
using System.Linq;
using System.Net;
using Rackspace.CloudNetworks.v2.Serialization;
using Rackspace.Synchronous;
using Rackspace.Testing;
using Xunit;

namespace Rackspace.CloudNetworks.v2
{
    public class SubnetTests
    {
        private readonly CloudNetworkService _cloudNetworkService;

        public SubnetTests()
        {
            _cloudNetworkService = new CloudNetworkService(Stubs.IdentityService, "region");
        }

        [Fact]
        public void ListSubnets()
        {
            using (var httpTest = new HttpTest())
            {
                Identifier subnetId = Guid.NewGuid();
                httpTest.RespondWithJson(new SubnetCollection(new[] {new Subnet {Id = subnetId}}));

                var subnets = _cloudNetworkService.ListSubnets();

                httpTest.ShouldHaveCalled("*/subnets");
                Assert.NotNull(subnets);
                Assert.Equal(1, subnets.Count());
                Assert.Equal(subnetId, subnets.First().Id);
            }
        }

        [Fact]
        public void CreateSubnet()
        {
            using (var httpTest = new HttpTest())
            {
                Identifier networkId = Guid.NewGuid();
                Identifier subnetId = Guid.NewGuid();
                httpTest.RespondWithJson(new Subnet { Id = subnetId });

                var definition = new SubnetCreateDefinition(networkId, IPVersion.IPv4, "10.0.0.0/24");
                var subnet = _cloudNetworkService.CreateSubnet(definition);
                
                httpTest.ShouldHaveCalled("*/subnets");
                Assert.NotNull(subnet);
                Assert.Equal(subnetId, subnet.Id);
            }
        }
        
        [Fact]
        public void GetSubnets()
        {
            using (var httpTest = new HttpTest())
            {
                Identifier subnetId = Guid.NewGuid();
                httpTest.RespondWithJson(new Subnet { Id = subnetId });

                var subnet = _cloudNetworkService.GetSubnet(subnetId);

                httpTest.ShouldHaveCalled("*/subnets/" + subnetId);
                Assert.NotNull(subnet);
                Assert.Equal(subnetId, subnet.Id);
            }
        }

        [Fact]
        public void DeleteSubnet()
        {
            using (var httpTest = new HttpTest())
            {
                Identifier subnetId = Guid.NewGuid();
                httpTest.RespondWith((int)HttpStatusCode.NoContent, "All gone!");

                _cloudNetworkService.DeleteSubnet(subnetId);

                httpTest.ShouldHaveCalled("*/subnets/" + subnetId);
            }
        }

        [Fact]
        public void WhenDeleteSubnet_Returns404NotFound_ShouldConsiderRequestSuccessful()
        {
            using (var httpTest = new HttpTest())
            {
                Identifier subnetId = Guid.NewGuid();
                httpTest.RespondWith((int)HttpStatusCode.NotFound, "Not here, boss...");

                _cloudNetworkService.DeleteSubnet(subnetId);

                httpTest.ShouldHaveCalled("*/subnets/" + subnetId);
            }
        }

        [Fact]
        public void UpdateSubnet()
        {
            using (var httpTest = new HttpTest())
            {
                Identifier subnetId = Guid.NewGuid();
                httpTest.RespondWithJson(new Subnet { Id = subnetId });

                var definition = new SubnetUpdateDefinition { Name = "new subnet name" };
                var subnet = _cloudNetworkService.UpdateSubnet(subnetId, definition);

                httpTest.ShouldHaveCalled("*/subnets/" + subnetId);
                Assert.NotNull(subnet);
                Assert.Equal(subnetId, subnet.Id);
            }
        }
    }
}
