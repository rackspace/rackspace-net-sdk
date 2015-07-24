using System.Collections.Generic;
using OpenStack.Synchronous.Extensions;
using Rackspace.CloudNetworks.v2;

// ReSharper disable once CheckNamespace
namespace Rackspace.Synchronous
{
    /// <summary>
    /// Provides synchronous extention methods for a <see cref="CloudNetworkService"/> instance.
    /// </summary>
    public static class CloudNetworkServiceExtensions
    {
        /// <inheritdoc cref="OpenStack.Synchronous.NetworkingServiceExtensions.ListNetworks" />
        public static IEnumerable<Network> ListNetworks(this CloudNetworkService svc)
        {
            return svc.ListNetworksAsync().ForceSynchronous();
        }

        /// <inheritdoc cref="OpenStack.Synchronous.NetworkingServiceExtensions.GetNetwork" />
        public static Network GetNetwork(this CloudNetworkService svc, string networkId)
        {
            return svc.GetNetworkAsync(networkId).ForceSynchronous();
        }

        /// <inheritdoc cref="OpenStack.Synchronous.NetworkingServiceExtensions.CreateNetwork" />
        public static Network CreateNetwork(this CloudNetworkService svc, NetworkDefinition network)
        {
            return svc.CreateNetworkAsync(network).ForceSynchronous();
        }

        /// <inheritdoc cref="OpenStack.Synchronous.NetworkingServiceExtensions.UpdateNetwork" />
        public static Network UpdateNetwork(this CloudNetworkService svc, string networkId, NetworkDefinition network)
        {
            return svc.UpdateNetworkAsync(networkId, network).ForceSynchronous();
        }

        /// <inheritdoc cref="OpenStack.Synchronous.NetworkingServiceExtensions.DeleteNetwork" />
        public static void DeleteNetwork(this CloudNetworkService svc, string networkId)
        {
            svc.DeleteNetworkAsync(networkId).ForceSynchronous();
        }
    }
}