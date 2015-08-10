using System.Collections.Generic;
using net.openstack.Core.Domain;
using Newtonsoft.Json;
using Rackspace.Serialization;

namespace Rackspace.CloudNetworks.v2.Serialization
{
    /// <inheritdoc cref="OpenStack.Networking.v2.Serialization.NetworkCollection"/>
    public class NetworkCollection : Page<Network>
    {
        /// <summary>
        /// The requested networks.
        /// </summary>
        [JsonProperty("networks")]
        public IList<Network> Networks
        {
            get { return Items; }
            set { Items = value; }
        }

        /// <summary>
        /// The paging navigation links.
        /// </summary>
        [JsonProperty("networks_links")]
        public IList<Link> NetworksLinks
        {
            get { return Links; }
            set { Links = value; }
        }
    }
}
