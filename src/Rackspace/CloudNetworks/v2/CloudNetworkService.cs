using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Extensions;
using Flurl.Http;
using OpenStack.Authentication;
using OpenStack.Synchronous.Extensions;

namespace Rackspace.CloudNetworks.v2
{
    /// <summary>
    /// The Rackspace Cloud Networks service.
    /// </summary>
    /// <seealso href="http://api.rackspace.com/api-ref-networks.html">Cloud Networks API v2 Reference</seealso>
    /// <seealso href="http://docs.rackspace.com/networks/api/v2/cn-gettingstarted/content/ch_preface.html">Cloud Networks Getting Started</seealso>
    public class CloudNetworkService
    {
        private readonly OpenStack.Networking.v2.NetworkingApiBuilder _networkingApiBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="CloudNetworkService"/> class.
        /// </summary>
        /// <param name="authenticationProvider">The authentication provider.</param>
        /// <param name="region">The region.</param>
        public CloudNetworkService(IAuthenticationProvider authenticationProvider, string region)
        {
            _networkingApiBuilder = new OpenStack.Networking.v2.NetworkingApiBuilder(authenticationProvider, region);
        }

        /// <inheritdoc cref="OpenStack.Networking.v2.NetworkingApiBuilder.ListNetworksAsync" />
        public Task<IEnumerable<Network>> ListNetworksAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return _networkingApiBuilder
                .ListNetworksAsync(cancellationToken)
                .SendAsync()
                .ReceiveJson<NetworkCollection>()
                .AsEnumerable<NetworkCollection, Network>();
        }

        /// <inheritdoc cref="OpenStack.Networking.v2.NetworkingApiBuilder.GetNetworkAsync" />
        public Task<Network> GetNetworkAsync(string networkId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _networkingApiBuilder
                .GetNetworkAsync(networkId, cancellationToken)
                .SendAsync()
                .ReceiveJson<Network>();
        }

        /// <inheritdoc cref="OpenStack.Networking.v2.NetworkingApiBuilder.CreateNetworkAsync" />
        public Task<Network> CreateNetworkAsync(NetworkDefinition network, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _networkingApiBuilder
                .CreateNetworkAsync(network, cancellationToken)
                .SendAsync()
                .ReceiveJson<Network>();
        }

        /// <inheritdoc cref="OpenStack.Networking.v2.NetworkingApiBuilder.CreateNetworksAsync" />
        public Task<IEnumerable<Network>> CreateNetworksAsync(IEnumerable<NetworkDefinition> networks, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _networkingApiBuilder
                .CreateNetworksAsync(networks, cancellationToken)
                .SendAsync()
                .ReceiveJson<NetworkCollection>()
                .AsEnumerable<NetworkCollection, Network>();
        }

        /// <inheritdoc cref="OpenStack.Networking.v2.NetworkingApiBuilder.UpdateNetworkAsync" />
        public Task<Network> UpdateNetworkAsync(string networkId, NetworkDefinition network, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _networkingApiBuilder
                .UpdateNetworkAsync(networkId, network, cancellationToken)
                .SendAsync()
                .ReceiveJson<Network>();
        }

        /// <inheritdoc cref="OpenStack.Networking.v2.NetworkingApiBuilder.DeleteNetworkAsync" />
        public Task DeleteNetworkAsync(string networkId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _networkingApiBuilder
                .DeleteNetworkAsync(networkId, cancellationToken)
                .SendAsync();
        }
    }
}
