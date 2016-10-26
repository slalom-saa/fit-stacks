using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Shouldly;
using Slalom.Stacks.Configuration;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Messaging.Validation;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Validation;
using Xunit;

namespace Slalom.Stacks.IntegrationTests
{
    public class InRoleSecurityRuleValidationShould
    {
        public class TestEvent : Event
        {
        }

        public class TestCommand : Command<TestEvent>
        {
        }

        public class TestCommandHandler : CommandHandler<TestCommand, TestEvent>
        {
            public override Task<TestEvent> Handle(TestCommand command)
            {
                return Task.FromResult(new TestEvent());
            }
        }

        public class must_be_administrator : InRoleSecurityRule<TestCommand>
        {
            public must_be_administrator()
                : base("Administrator", "no message")
            {
            }
        }

        [Fact]
        public async void ReturnErrorWhenNotInRole()
        {
            using (var container = new Container(this))
            {
                container.Register<ExecutionContext>(new LocalExecutionContext());

                var bus = container.Resolve<IMessageBus>();

                var result = await bus.Send(new TestCommand());

                result.ValidationErrors.Count(e => e.ErrorType == ValidationErrorType.Security).ShouldBe(1);
            }
        }

        [Fact]
        public async void ReturnSuccessWhenInRole()
        {
            using (var container = new Container(this))
            {
                container.Register<IExecutionContextResolver>(new LocalExecutionContextResolver(new LocalExecutionContext("Administrator", new Claim(ClaimTypes.Role, "Administrator"))));

                var bus = container.Resolve<IMessageBus>();

                var result = await bus.Send(new TestCommand());

                result.ValidationErrors.Count(e => e.ErrorType == ValidationErrorType.Security).ShouldBe(0);
            }
        }
    }
}