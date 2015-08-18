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
        private readonly CloudServersTestDataManager _testData;

        public RackConnectServiceTests(ITestOutputHelper testLog)
        {
            var testOutput = new XunitTraceListener(testLog);
            Trace.Listeners.Add(testOutput);
            RackspaceNet.Tracing.Http.Listeners.Add(testOutput);

            var authenticationProvider = TestIdentityProvider.GetIdentityProvider("RackConnect");
            _rackConnectService = new RackConnectService(authenticationProvider, "LON");

            _testData = new CloudServersTestDataManager(authenticationProvider);
        }

        public void Dispose()
        {
            Trace.Listeners.Clear();
            RackspaceNet.Tracing.Http.Listeners.Clear();

            _testData.Dispose();
        }

        [Fact]
        public async Task AssignPublicIPTest()
        {
            Trace.WriteLine("Looking up the RackConnect network...");
            var network = (await _rackConnectService.ListNetworksAsync()).First();

            Trace.Write("Creating a test cloud server...");
            var server = _testData.CreateServer(network.Id);
            Trace.WriteLine(server.Id);

            Trace.Write("Assigning a public ip address... ");
            var ip = await _rackConnectService.AssignPublicIPAsync(server.Id);
            await ip.WaitUntilActiveAsync();
            Trace.WriteLine(ip.PublicIPv4Address);

            Assert.NotNull(ip);
            Assert.Equal(server.Id, ip.Server.ServerId);
            Assert.NotNull(ip.PublicIPv4Address);
            Assert.Equal(PublicIPStatus.Active, ip.Status);
        }

        [Fact]
        public async Task ListNetworks()
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
    }
}