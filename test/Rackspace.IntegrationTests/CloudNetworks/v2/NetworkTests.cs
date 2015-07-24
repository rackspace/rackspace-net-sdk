using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Rackspace.Synchronous;
using Xunit;
using Xunit.Abstractions;

namespace Rackspace.CloudNetworks.v2
{
    public class NetworkTests
    {
        private readonly CloudNetworkService _cloudNetworkService;

        public NetworkTests(ITestOutputHelper testLog)
        {
            var xunitOutput = new XUnitTraceListener(testLog);
            RackspaceNet.Tracing.Http.Listeners.Add(xunitOutput);
            Trace.Listeners.Add(xunitOutput);

            var authenticationProvider = TestIdentityProvider.GetIdentityProvider();
            _cloudNetworkService = new CloudNetworkService(authenticationProvider, "IAD");
        }

        [Fact]
        public async void CreateUpdateThenDeleteNetwork()
        {
            var definition = BuildNetworkDefinition();
            Trace.WriteLine(string.Format("Creating Network named {0}", definition.Name));
            var network = await _cloudNetworkService.CreateNetworkAsync(definition);
            Trace.WriteLine(string.Format("Network was created: {0}", network.Id));

            try
            {
                Trace.WriteLine("Verifying network matches requested definition...");
                Assert.Equal(definition.Name, network.Name);
                Assert.True(network.IsUp);

                Trace.WriteLine("Updating the network...");
                definition.Name += "updated";
                network = await _cloudNetworkService.UpdateNetworkAsync(network.Id, definition);

                Trace.WriteLine("Verifying network matches updated definition...");
                Assert.Equal(definition.Name, network.Name);
            }
            finally
            {
                Trace.WriteLine("Cleaning up any test data...");

                Trace.WriteLine("Removing the network...");
                _cloudNetworkService.DeleteNetwork(network.Id);
                Trace.WriteLine("The service was cleaned up sucessfully.");
            }
        }

        [Fact]
        public async void FindNetworks()
        {
            var networkIds = new List<string>();
            try
            {
                var create1 = CreateNetwork().ContinueWith(t => networkIds.Add(t.Result.Id));
                var create2 = CreateNetwork().ContinueWith(t => networkIds.Add(t.Result.Id));
                var create3 = CreateNetwork().ContinueWith(t => networkIds.Add(t.Result.Id));
                await Task.WhenAll(create1, create2, create3);

                Trace.WriteLine("Listing networks...");
                var allNetworks = await _cloudNetworkService.ListNetworksAsync();
                Assert.All(networkIds, id => allNetworks.Any(n => n.Id == id));

                Trace.WriteLine("Retrieving a network...");
                var networkId = networkIds.First();
                var network = await _cloudNetworkService.GetNetworkAsync(networkId);
                Assert.NotNull(network);
                Assert.Equal(networkId, network.Id);
            }
            finally
            {
                Trace.WriteLine("Cleaning up any test data...");

                Trace.WriteLine("Removing the networks...");
                var deletes = networkIds.Select(id => _cloudNetworkService
                    .DeleteNetworkAsync(id)
                    .ContinueWith(t => Trace.WriteLine(string.Format("Network was deleted: {0}", id))))
                    .ToArray();

                Task.WaitAll(deletes);
                Trace.WriteLine("The networks were cleaned up sucessfully.");

            }
        }

        private static NetworkDefinition BuildNetworkDefinition()
        {
            return new NetworkDefinition { Name = string.Format("ci-test-{0}", Guid.NewGuid()) };
        }

        private async Task<Network> CreateNetwork()
        {
            var definition = BuildNetworkDefinition();
            Trace.WriteLine(string.Format("Creating Network named {0}", definition.Name));
            var network = await _cloudNetworkService.CreateNetworkAsync(definition);
            Trace.WriteLine(string.Format("Network was created: {0}", network.Id));
            return network;
        }
    }
}
