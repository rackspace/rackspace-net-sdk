using System.Collections.Generic;
using OpenStack.Serialization;

namespace Rackspace.CloudNetworks.v2
{
    /// <summary>
    /// Represents a collection of network resources returned by the <see cref="CloudNetworkService"/>.
    /// <para>Intended for custom implementations and stubbing responses in unit tests.</para>
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
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