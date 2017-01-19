using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Serialization;

namespace WebApplication1.Formatting
{
    public class ApiContractResolver : CamelCasePropertyNamesContractResolver
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
            var propertyInfo = member as PropertyInfo;
            if (propertyInfo?.GetCustomAttributes<IgnoreAttribute>().Any() ?? false)
            {
                prop.Ignored = true;
            }
            else if (propertyInfo?.GetCustomAttributes<SecureAttribute>().Any() ?? false)
            {
                prop.Converter = new SecureJsonConverter();                
            }           
            else if (typeof(IEvent).IsAssignableFrom(propertyInfo.DeclaringType) && propertyInfo.DeclaringType == typeof(Event))
            {
                prop.Ignored = true;
            }
            return prop;
        }
    }
}