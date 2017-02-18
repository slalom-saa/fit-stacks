using System;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Serialization;

namespace Slalom.Stacks.Messaging.Serialization
{
    /// <summary>
    /// A JSON Contract Resolver for <see cref="Event"/> instances.
    /// </summary>
    public class EventContractResolver : BaseContractResolver
    {
        /// <summary>
        /// Creates a <see cref="T:Newtonsoft.Json.Serialization.JsonProperty" /> for the given <see cref="T:System.Reflection.MemberInfo" />.
        /// </summary>
        /// <param name="member">The member to create a <see cref="T:Newtonsoft.Json.Serialization.JsonProperty" /> for.</param>
        /// <param name="memberSerialization">The member's parent <see cref="T:Newtonsoft.Json.MemberSerialization" />.</param>
        /// <returns>A created <see cref="T:Newtonsoft.Json.Serialization.JsonProperty" /> for the given <see cref="T:System.Reflection.MemberInfo" />.</returns>
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var prop = base.CreateProperty(member, memberSerialization);
            if ((member as PropertyInfo).GetCustomAttributes<IgnoreAttribute>().Any())
            {
                prop.Ignored = true;
                return prop;
            }
            var declaringType = (member as PropertyInfo)?.DeclaringType;
            if (declaringType == typeof(Event))
            {
                prop.Ignored = true;
                return prop;
            }
            return base.CreateProperty(member, memberSerialization);
        }
    }
}
