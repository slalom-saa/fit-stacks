using System;
using Shouldly;
using Slalom.FitStacks.Messaging;
using Xunit;

namespace Slalom.FitStacks.UnitTests
{
    public class Command_ConstructorShould
    {
        public class TestCommand : Command<string>
        {
        }

        [Fact]
        public void SetTimeStamp()
        {
            var command = new TestCommand();

            command.TimeStamp.ShouldBeGreaterThan(default(DateTimeOffset));
        }

        [Fact]
        public void SetId()
        {
            var command = new TestCommand();

            command.Id.ShouldNotBe(Guid.Empty);
        }
    }
}