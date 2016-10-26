using System;
using System.Linq;
using System.Security.Claims;
using Newtonsoft.Json;

namespace Slalom.Stacks.Messaging.Serialization
{
    /// <summary>
    /// Allows for serialization and deserialziation of <see cref="ClaimsPrincipal"/> instances.
    /// </summary>
    /// <seealso cref="Newtonsoft.Json.JsonConverter" />
    public class ClaimsPrincipalConverter : JsonConverter
    {
        private class ClaimsPrincipalHolder
        {
            public string AuthenticationType { get; set; }

            public ClaimHolder[] Claims { get; set; }
        }

        private class ClaimHolder
        {
            public string Type { get; set; }

            public string Value { get; set; }
        }

        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns><c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.</returns>
        public override bool CanConvert(Type objectType)
        {
            return typeof(ClaimsPrincipal) == objectType;
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The object value.</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var source = serializer.Deserialize<ClaimsPrincipalHolder>(reader);
            if (source == null) return null;

            var claims = source.Claims.Select(x => new Claim(x.Type, x.Value));
            var id = new ClaimsIdentity(claims, source.AuthenticationType);
            var target = new ClaimsPrincipal(id);
            return target;
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var source = (ClaimsPrincipal)value;

            var target = new ClaimsPrincipalHolder
            {
                AuthenticationType = source.Identity.AuthenticationType,
                Claims = source.Claims.Select(x => new ClaimHolder { Type = x.Type, Value = x.Value }).ToArray()
            };
            serializer.Serialize(writer, target);
        }
    }
}