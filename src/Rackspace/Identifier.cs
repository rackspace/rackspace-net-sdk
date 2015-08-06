using System;
using Newtonsoft.Json;
using Rackspace.Serialization;

namespace Rackspace
{
    /// <inheritdoc />
    [JsonConverter(typeof(IdentifierConverter))]
    public class Identifier : OpenStack.Identifier
    {
        /// <inheritdoc />
        public Identifier(string id) : base(id)
        {
        }

        /// <inheritdoc />
        public Identifier(Guid id) : base(id)
        {
        }

        #region Conversions
        /// <summary>
        /// Performs an implicit conversion from <see cref="Guid"/> to <see cref="Identifier"/>.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator Identifier(Guid id)
        {
            return new Identifier(id);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Identifier"/> to <see cref="String"/>.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator string (Identifier id)
        {
            return id == null ? string.Empty : id.ToString();
        }

        /// <summary>
        /// Performs an explicit conversion from <see cref="String"/> to <see cref="Identifier"/>.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static explicit operator Identifier(string id)
        {
            return new Identifier(id);
        }
        #endregion
    }
}