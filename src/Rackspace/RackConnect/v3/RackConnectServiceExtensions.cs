using System;
using System.Collections.Generic;
using OpenStack.Synchronous.Extensions;
using Rackspace.RackConnect.v3;

// ReSharper disable once CheckNamespace
namespace Rackspace.Synchronous
{
    /// <summary>
    /// Provides synchronous extention methods for an <see cref="RackConnectService"/> instance.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    public static class RackConnectServiceExtensions
    {
        /// <inheritdoc cref="RackConnectService.ListPublicIPsAsync"/>
        public static IEnumerable<PublicIP> ListPublicIPs(this RackConnectService rackConnectService)
        {
            return rackConnectService.ListPublicIPsAsync().ForceSynchronous();
        }

        /// <inheritdoc cref="RackConnectService.ListNetworksAsync"/>
        public static IEnumerable<NetworkReference> ListNetworks(this RackConnectService rackConnectService)
        {
            return rackConnectService.ListNetworksAsync().ForceSynchronous();
        }

        /// <inheritdoc cref="RackConnectService.GetNetworkAsync"/>
        public static NetworkReference GetNetwork(this RackConnectService rackConnectService, Identifier networkId)
        {
            return rackConnectService.GetNetworkAsync(networkId).ForceSynchronous();
        }

        /// <inheritdoc cref="RackConnectService.AssignPublicIPAsync"/>
        public static PublicIP AssignPublicIP(this RackConnectService rackConnectService, string serverId)
        {
            return rackConnectService.AssignPublicIPAsync(serverId).ForceSynchronous();
        }

        /// <inheritdoc cref="RackConnectService.WaitUntilPublicIPIsActiveAsync"/>
        public static void WaitUntilActive(this PublicIP publicIP, TimeSpan? refreshDelay = null, TimeSpan? timeout = null, IProgress<bool> progress = null)
        {
            publicIP.WaitUntilActiveAsync(refreshDelay, timeout, progress).ForceSynchronous();
        }
    }
}