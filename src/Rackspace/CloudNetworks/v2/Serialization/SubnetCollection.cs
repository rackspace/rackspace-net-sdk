using System.Collections.Generic;
using net.openstack.Core.Domain;
using Newtonsoft.Json;
using Rackspace.Serialization;

namespace Rackspace.CloudNetworks.v2.Serialization
{
    /// <inheritdoc cref="OpenStack.Networking.v2.Serialization.SubnetCollection"/>
    public class SubnetCollection : Page<Subnet>
    {
        /// <summary>
        /// The requested subnets.
        /// </summary>
        [JsonProperty("subnets")]
        public IList<Subnet> Subnets
        {
            get { return Items; }
            set { Items = value; }
        }

        /// <summary>
        /// The paging navigation links.
        /// </summary>
        [JsonProperty("subnets_links")]
        public IList<Link> SubnetsLinks
        {
            get { return Links; }
            set { Links = value; }
        }
    }
}