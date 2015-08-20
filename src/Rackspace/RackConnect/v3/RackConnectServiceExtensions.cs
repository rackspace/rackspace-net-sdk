using System;
using System.Collections.Generic;
using OpenStack.Synchronous.Extensions;
using Rackspace.RackConnect.v3;

// ReSharper disable once CheckNamespace
namespace Rackspace.Synchronous
{
    /// <summary>
    /// Provides synchronous extention methods for the <see cref="RackConnectService"/>.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    public static class RackConnectServiceExtensions
    {
        #region Networks
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
        #endregion

        #region Public IPs
        /// <inheritdoc cref="RackConnectService.ListPublicIPsAsync"/>
        public static IEnumerable<PublicIP> ListPublicIPs(this RackConnectService rackConnectService, string serverId = null)
        {
            return rackConnectService.ListPublicIPsAsync(serverId).ForceSynchronous();
        }

        /// <inheritdoc cref="RackConnectService.GetPublicIPAsync"/>
        public static PublicIP GetPublicIP(this RackConnectService rackConnectService, Identifier publicIPId)
        {
            return rackConnectService.GetPublicIPAsync(publicIPId).ForceSynchronous();
        }

        /// <inheritdoc cref="RackConnectService.AssignPublicIPAsync"/>
        public static PublicIP AssignPublicIP(this RackConnectService rackConnectService, string serverId)
        {
            return rackConnectService.AssignPublicIPAsync(serverId).ForceSynchronous();
        }

        /// <inheritdoc cref="PublicIP.WaitUntilActiveAsync"/>
        public static void WaitUntilActive(this PublicIP publicIP, TimeSpan? refreshDelay = null, TimeSpan? timeout = null, IProgress<bool> progress = null)
        {
            publicIP.WaitUntilActiveAsync(refreshDelay, timeout, progress).ForceSynchronous();
        }

        /// <inheritdoc cref="PublicIP.WaitUntilRemovedAsync"/>
        public static void WaitUntilRemoved(this PublicIP publicIP, TimeSpan? refreshDelay = null, TimeSpan? timeout = null, IProgress<bool> progress = null)
        {
            publicIP.WaitUntilRemovedAsync(refreshDelay, timeout, progress).ForceSynchronous();
        }

        /// <inheritdoc cref="RackConnectService.RemovePublicIPAsync"/>
        public static void RemovePublicIP(this RackConnectService rackConnectService, Identifier publicIPId)
        {
            rackConnectService.RemovePublicIPAsync(publicIPId).ForceSynchronous();
        }

        /// <inheritdoc cref="PublicIP.RemoveAsync"/>
        public static void Remove(this PublicIP publicIP)
        {
            publicIP.RemoveAsync().ForceSynchronous();
        }
        #endregion
    }
}