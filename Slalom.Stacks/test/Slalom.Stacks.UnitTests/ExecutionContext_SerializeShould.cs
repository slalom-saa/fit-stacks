using System;
using System.Linq;
using System.Security.Claims;
using Newtonsoft.Json;
using Shouldly;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Serialization;
using Xunit;

namespace Slalom.Stacks.UnitTests
{
    public class ExecutionContext_SerializeShould
    {
        [Theory, InlineData("name")]
        public void KeepUserInformation(string name)
        {
            JsonConvert.DefaultSettings = () => DefaultSerializationSettings.Instance;

            var context = new LocalExecutionContext(name);

            var serialized = JsonConvert.SerializeObject(context);

            var deserialized = JsonConvert.DeserializeObject<ExecutionContext>(serialized);

            deserialized.User.Identity.Name.ShouldBe(name);
        }
    }
}