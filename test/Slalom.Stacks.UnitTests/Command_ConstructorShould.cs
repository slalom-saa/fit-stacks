using System;
using Shouldly;
using Slalom.Stacks.Communication;
using Xunit;

namespace Slalom.Stacks.UnitTests
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

            command.Id.ShouldNotBeNull();
        }
    }
}