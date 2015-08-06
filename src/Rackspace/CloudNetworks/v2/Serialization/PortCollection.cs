using System.Collections.Generic;
using OpenStack.Serialization;

namespace Rackspace.CloudNetworks.v2.Serialization
{
    /// <inheritdoc cref="OpenStack.Networking.v2.Serialization.PortCollection"/>
    [JsonConverterWithConstructor(typeof(RootWrapperConverter), "ports")]
    public class PortCollection : List<Port>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PortCollection"/> class.
        /// </summary>
        public PortCollection()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PortCollection"/> class.
        /// </summary>
        /// <param name="ports">The networks.</param>
        public PortCollection(IEnumerable<Port> ports) : base(ports)
        {
        }
    }
}