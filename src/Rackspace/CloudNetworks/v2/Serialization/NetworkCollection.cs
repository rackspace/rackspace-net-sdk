using System.Collections.Generic;
using OpenStack.Serialization;

namespace Rackspace.CloudNetworks.v2.Serialization
{
    /// <inheritdoc cref="OpenStack.Networking.v2.Serialization.NetworkCollection"/>
    [JsonConverterWithConstructor(typeof(RootWrapperConverter), "networks")]
    public class NetworkCollection : List<Network>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkCollection"/> class.
        /// </summary>
        public NetworkCollection()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkCollection"/> class.
        /// </summary>
        /// <param name="networks">The networks.</param>
        public NetworkCollection(IEnumerable<Network> networks) : base(networks)
        {
        }
    }
}
