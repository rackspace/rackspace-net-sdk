using System.Runtime.Serialization;
using Newtonsoft.Json;
using OpenStack.Serialization;

namespace Rackspace.RackConnect.v3
{
    /// <summary>
    /// <see cref="PublicIP"/> Status
    /// </summary>
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum PublicIPStatus
    {
        /// <summary>
        /// The status is unknown.
        /// </summary>
        [EnumMember(Value = "UNKNOWN")]
        Unknown,

        /// <summary>
        /// The public IP address is active.
        /// </summary>
        [EnumMember(Value = "ACTIVE")]
        Active,

        /// <summary>
        /// The public IP address is being added.
        /// </summary>
        [EnumMember(Value = "ADDING")]
        Adding,

        /// <summary>
        /// The public IP address is being updated.
        /// </summary>
        [EnumMember(Value = "UPDATING")]
        Updating,

        /// <summary>
        /// The server associated with the public IP is still being built.
        /// </summary>
        [EnumMember(Value = "PENDING_BUILD")]
        PendingBuild,

        /// <summary>
        /// The public IP address add operation failed.
        /// </summary>
        [EnumMember(Value = "ADD_FAILED")]
        AddFailed,

        /// <summary>
        /// The public IP address update operation failed.
        /// </summary>
        [EnumMember(Value = "UPDATE_FAILED")]
        UpdateFailed,

        /// <summary>
        /// The public IP address remove operation failed.
        /// </summary>
        [EnumMember(Value = "REMOVE_FAILED")]
        RemoveFailed,

        /// <summary>
        /// The public IP address has been removed.
        /// </summary>
        [EnumMember(Value = "REMOVED")]
        Removed
    }
}