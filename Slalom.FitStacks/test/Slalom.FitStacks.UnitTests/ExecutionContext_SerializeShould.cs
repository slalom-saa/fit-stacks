using System;
using System.Linq;
using System.Security.Claims;
using Newtonsoft.Json;
using Shouldly;
using Slalom.FitStacks.Messaging;
using Slalom.FitStacks.Runtime;
using Slalom.FitStacks.Serialization;
using Xunit;

namespace Slalom.FitStacks.UnitTests
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