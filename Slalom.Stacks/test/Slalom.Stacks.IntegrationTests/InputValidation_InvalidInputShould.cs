using System;
using Shouldly;
using System.ComponentModel;
using System.Linq;
using Slalom.Stacks.Configuration;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Messaging.Validation;
using Slalom.Stacks.Runtime;
using Xunit;

namespace Slalom.Stacks.IntegrationTests
{
    public class InputValidation_InvalidInputShould
    {
        public class TestCommand : Command<string>
        {
            public string Name { get; set; }

            public TestCommand(string name)
            {
                this.Name = name;
            }
        }

        public class test_command_valid : InputValidationRuleSet<TestCommand>
        {
            public test_command_valid()
            {
                this.Add(e => e.Name)
                    .NotNullOrWhiteSpace("Must have a name.");
            }
        }

        [Fact]
        public async void HaveValidationMessage()
        {
            using (var container = new Container(this))
            {
                container.Register<ExecutionContext>(new LocalExecutionContext());

                var bus = container.Resolve<IMessageBus>();

                var result = await bus.Send(new TestCommand(null));

                result.ValidationErrors.Count().ShouldBe(1);
            }
        }
    }
}
