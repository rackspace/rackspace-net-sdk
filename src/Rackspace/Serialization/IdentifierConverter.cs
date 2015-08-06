using System;
using Newtonsoft.Json;

namespace Rackspace.Serialization
{
    internal class IdentifierConverter : OpenStack.Serialization.IdentifierConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.Null:
                    return null;
                case JsonToken.String:
                    var id = reader.Value.ToString();
                    return string.IsNullOrEmpty(id) ? null : new Identifier(id);
            }

            throw new JsonSerializationException($"Unexpected token when deserializing {objectType.FullName}");
        }
    }
}