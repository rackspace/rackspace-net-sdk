using System.Collections.Generic;
using OpenStack.Serialization;

namespace Rackspace.CloudNetworks.v2.Serialization
{
    /// <inheritdoc cref="OpenStack.Networking.v2.Serialization.SubnetCollection"/>
    [JsonConverterWithConstructor(typeof(RootWrapperConverter), "subnets")]
    public class SubnetCollection : List<Subnet>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubnetCollection"/> class.
        /// </summary>
        public SubnetCollection()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SubnetCollection"/> class.
        /// </summary>
        /// <param name="subnets">The networks.</param>
        public SubnetCollection(IEnumerable<Subnet> subnets) : base(subnets)
        {
        }
    }
}