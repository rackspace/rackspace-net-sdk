using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Rackspace.CloudServers.v2;
using Xunit;
using Xunit.Abstractions;

namespace Rackspace.RackConnect.v3
{
    public class RackConnectServiceTests : IDisposable
    {
        private readonly RackConnectService _rackConnectService;
        private readonly RackConnectTestDataManager _testData;

        public RackConnectServiceTests(ITestOutputHelper testLog)
        {
            var testOutput = new XunitTraceListener(testLog);
            Trace.Listeners.Add(testOutput);
            RackspaceNet.Tracing.Http.Listeners.Add(testOutput);

            var authenticationProvider = TestIdentityProvider.GetIdentityProvider("RackConnect");
            _rackConnectService = new RackConnectService(authenticationProvider, "LON");

            _testData = new RackConnectTestDataManager(_rackConnectService, authenticationProvider);
        }

        public void Dispose()
        {
            Trace.Listeners.Clear();
            RackspaceNet.Tracing.Http.Listeners.Clear();

            _testData.Dispose();
        }

        [Fact]
        public async Task ProvisionPublicIPTest()
        {
            Trace.WriteLine("Looking up the RackConnect network...");
            var network = (await _rackConnectService.ListNetworksAsync()).First();

            Trace.Write("Creating a test cloud server...");
            var server = _testData.CreateServer(network.Id);
            Trace.WriteLine(server.Id);

            Trace.Write("Assigning a public ip address to the test cloud server... ");
            var ipRequest = new PublicIPDefinition {ServerId = server.Id, ShouldRetain = true};
            var ip = await _rackConnectService.AssignPublicIPAsync(server.Id);
            var ip = await _testData.ProvisionPublicIP(ipRequest);
            await ip.WaitUntilActiveAsync();
            Trace.WriteLine(ip.PublicIPv4Address);

            Assert.NotNull(ip);
            Assert.Equal(server.Id, ip.Server.ServerId);
            Assert.NotNull(ip.PublicIPv4Address);
            Assert.Equal(PublicIPStatus.Active, ip.Status);

            Trace.WriteLine("Retrieving public IPs assigned to the test cloud server...");
            var ips = await _rackConnectService.ListPublicIPsAsync(server.Id);
            Assert.NotNull(ips);
            Assert.True(ips.Any(x => x.Id == ip.Id));

            Trace.WriteLine("Removing public IP from test cloud server...");
            await ip.RemoveAsync();
            await ip.WaitUntilRemovedAsync();

            Trace.WriteLine($"Verifying that {ip.PublicIPv4Address} is no longer assigned...");
            ips = await _rackConnectService.ListPublicIPsAsync(server.Id);
            Assert.NotNull(ips);
            Assert.False(ips.Any(x => x.Id == ip.Id));
        }

        [Fact]
        public async Task ListNetworksTest()
        {
            Trace.WriteLine("Listing RackConnect networks...");
            var networks = await _rackConnectService.ListNetworksAsync();

            Assert.NotNull(networks);
            var network = networks.FirstOrDefault();
            Assert.NotNull(network);
            Assert.NotNull(network.Id);
            Assert.NotNull(network.Name);
            Assert.NotNull(network.CIDR);
            Assert.NotNull(network.Created);
        }

        [Fact]
        public async Task GetNetworkTest()
        {
            Trace.WriteLine("Listing RackConnect networks...");
            var networks = await _rackConnectService.ListNetworksAsync();
            Assert.NotNull(networks);
            var network = networks.FirstOrDefault();
            Assert.NotNull(network);

            Trace.WriteLine($"Retrieving RackConnect network ${network.Name}...");
            network = await _rackConnectService.GetNetworkAsync(network.Id);
            Assert.NotNull(network.Id);
            Assert.NotNull(network.Name);
            Assert.NotNull(network.CIDR);
            Assert.NotNull(network.Created);
        }
    }
}