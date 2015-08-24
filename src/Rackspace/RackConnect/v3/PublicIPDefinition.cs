using Newtonsoft.Json;

namespace Rackspace.RackConnect.v3
{
    /// <summary>
    /// Represents a request to the <see cref="RackConnectService"/> to provision a public IP address.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    public class PublicIPDefinition
    {
        [JsonProperty("cloud_server")]
        private dynamic CloudServer
        {
            get
            {
                return ServerId != null ? new {id = ServerId} : null;
            }
        }

        /// <summary>
        /// If specified, requests that the IP address be associated to the specified server.
        /// </summary>
        [JsonIgnore]
        public string ServerId { get; set; }
    }
}