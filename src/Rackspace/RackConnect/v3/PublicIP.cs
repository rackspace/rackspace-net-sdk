using System;
using System.Extensions;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Rackspace.RackConnect.v3
{
    /// <summary>
    /// Represents a public IP address resource of the <see cref="RackConnectService"/>.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    public class PublicIP
    {
        private RackConnectService _owner;

        internal void SetOwner(RackConnectService owner)
        {
            _owner = owner;
        }

        /// <summary>
        /// The public IP address identifier.
        /// </summary>
        [JsonProperty("id")]
        public Identifier Id { get; set; }

        /// <summary>
        /// Timestamp when the public IP address was allocated.
        /// </summary>
        [JsonProperty("created")]
        public DateTime Created { get; set; }

        /// <summary>
        /// The server to which the public IP address has been allocated.
        /// </summary>
        [JsonProperty("cloud_server")]
        public PublicIPServerAssociation Server { get; set; }

        /// <summary>
        /// The allocated public IP address (IPv4).
        /// </summary>
        [JsonProperty("public_ip_v4")]
        public string PublicIPv4Address { get; set; }

        /// <summary>
        /// The public IP address status.
        /// </summary>
        [JsonProperty("status")]
        public PublicIPStatus Status { get; set; }

        /// <summary>
        /// Provides additional information when <see cref="Status"/> is in a failed state.
        /// </summary>
        [JsonProperty("status_detail")]
        public string StatusDetails { get; set; }

        /// <summary>
        /// Timestamp when the public IP address allocation was last updated.
        /// </summary>
        [JsonProperty("updated")]
        public DateTime? Updated { get; set; }

        /// <inheritdoc cref="RackConnectService.WaitUntilPublicIPIsActiveAsync" />
        public async Task WaitUntilActiveAsync(TimeSpan? refreshDelay = null, TimeSpan? timeout = null, IProgress<bool> progress = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if(_owner == null)
                throw new InvalidOperationException("WaitUntilActiveAsync can only be used on instances which were created by the RackConnectService. Use RackConnectService.WaitUntilPublicIPIsActiveAsync instead.");

            var result = await _owner.WaitUntilPublicIPIsActiveAsync(Id, refreshDelay, timeout, progress, cancellationToken).ConfigureAwait(false);
            result.CopyProperties(this);
        }
    }
}