using System;
using System.Collections.Generic;
using Marvin.JsonPatch;
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
        public static IEnumerable<PublicIP> ListPublicIPs(this RackConnectService rackConnectService, ListPublicIPsFilter filter = null)
        {
            return rackConnectService.ListPublicIPsAsync(filter).ForceSynchronous();
        }

        /// <inheritdoc cref="RackConnectService.GetPublicIPAsync"/>
        public static PublicIP GetPublicIP(this RackConnectService rackConnectService, Identifier publicIPId)
        {
            return rackConnectService.GetPublicIPAsync(publicIPId).ForceSynchronous();
        }

        /// <inheritdoc cref="RackConnectService.ProvisionPublicIPAsync"/>
        public static PublicIP ProvisionPublicIP(this RackConnectService rackConnectService, PublicIPDefinition definition)
        {
            return rackConnectService.ProvisionPublicIPAsync(definition).ForceSynchronous();
        }

        /// <inheritdoc cref="RackConnectService.WaitUntilPublicIPIsActiveAsync"/>
        public static PublicIP WaitUntilPublicIPIsActive(this RackConnectService rackConnectService, Identifier publicIPId, TimeSpan? refreshDelay = null, TimeSpan? timeout = null, IProgress<bool> progress = null)
        {
            return rackConnectService.WaitUntilPublicIPIsActiveAsync(publicIPId, refreshDelay, timeout, progress).ForceSynchronous();
        }

        /// <inheritdoc cref="PublicIP.WaitUntilActiveAsync"/>
        public static void WaitUntilActive(this PublicIP publicIP, TimeSpan? refreshDelay = null, TimeSpan? timeout = null, IProgress<bool> progress = null)
        {
            publicIP.WaitUntilActiveAsync(refreshDelay, timeout, progress).ForceSynchronous();
        }

        /// <inheritdoc cref="RackConnectService.WaitUntilPublicIPIsRemovedAsync"/>
        public static void WaitUntilPublicIPIsRemoved(this RackConnectService rackConnectService, Identifier publicIPId, TimeSpan? refreshDelay = null, TimeSpan? timeout = null, IProgress<bool> progress = null)
        {
            rackConnectService.WaitUntilPublicIPIsRemovedAsync(publicIPId, refreshDelay, timeout, progress).ForceSynchronous();
        }

        /// <inheritdoc cref="PublicIP.WaitUntilRemovedAsync"/>
        public static void WaitUntilRemoved(this PublicIP publicIP, TimeSpan? refreshDelay = null, TimeSpan? timeout = null, IProgress<bool> progress = null)
        {
            publicIP.WaitUntilRemovedAsync(refreshDelay, timeout, progress).ForceSynchronous();
        }

        /// <inheritdoc cref="RackConnectService.UpdatePublicIPAsync"/>
        public static PublicIP UpdatePublicIP(this RackConnectService rackConnectService, Identifier publicIPId, PublicIPUpdateDefinition definition)
        {
            return rackConnectService.UpdatePublicIPAsync(publicIPId, definition).ForceSynchronous();
        }

        /// <inheritdoc cref="RackConnectService.DeletePublicIPAsync"/>
        public static void RemovePublicIP(this RackConnectService rackConnectService, Identifier publicIPId)
        {
            rackConnectService.RemovePublicIPAsync(publicIPId).ForceSynchronous();
        }

        /// <inheritdoc cref="PublicIP.RemoveAsync"/>
        public static void Remove(this PublicIP publicIP)
        {
            publicIP.RemoveAsync().ForceSynchronous();
        }

        /// <inheritdoc cref="PublicIP.AssignAsync"/>
        public static void Assign(this PublicIP publicIP, string serverId)
        {
            publicIP.AssignAsync(serverId).ForceSynchronous();
        }

        /// <inheritdoc cref="PublicIP.UnassignAsync"/>
        public static void Unassign(this PublicIP publicIP)
        {
            publicIP.UnassignAsync().ForceSynchronous();
        }
        #endregion
    }
}