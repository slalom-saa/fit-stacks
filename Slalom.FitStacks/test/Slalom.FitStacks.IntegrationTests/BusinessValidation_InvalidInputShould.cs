using System.Collections.Generic;
using Shouldly;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Slalom.FitStacks.Configuration;
using Slalom.FitStacks.Messaging;
using Slalom.FitStacks.Messaging.Validation;
using Slalom.FitStacks.Runtime;
using Slalom.FitStacks.Validation;
using Xunit;

namespace Slalom.FitStacks.IntegrationTests
{
    public class SecurityValidation_InvalidInputShould
    {
        public class TestCommand : Command<string>
        {
            public string Name { get; set; }

            public TestCommand(string name)
            {
                this.Name = name;
            }
        }

        public class test_command_valid : SecurityValidationRule<TestCommand>
        {
            protected override Task<ValidationError> Validate(TestCommand instance)
            {
                return Task.FromResult(new ValidationError("testing"));
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

                result.ValidationErrors.Count(e => e.ErrorType == ValidationErrorType.Security).ShouldBe(1);
            }
        }

        [Fact]
        public async void NotRunIfInputValidatinFails()
        {
            using (var container = new Container(this))
            {
                container.Register<ExecutionContext>(new LocalExecutionContext());

                var mock = new Mock<IInputValidationRule<TestCommand>>();
                mock.Setup(e => e.Validate(It.IsAny<TestCommand>(), It.IsAny<ExecutionContext>()))
                    .Returns(new[] { new ValidationError("none") });
                container.Register(mock.Object);

                var bus = container.Resolve<IMessageBus>();

                var result = await bus.Send(new TestCommand(null));

                result.ValidationErrors.Count(e => e.ErrorType == ValidationErrorType.Security).ShouldBe(0);
            }
        }
    }

    public class BusinessValidation_InvalidInputShould
    {
        public class TestCommand : Command<string>
        {
            public string Name { get; set; }

            public TestCommand(string name)
            {
                this.Name = name;
            }
        }

        public class test_command_valid : BusinessValidationRule<TestCommand>
        {
            protected override Task<ValidationError> Validate(TestCommand instance)
            {
                return Task.FromResult(new ValidationError("testing"));
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

        [Fact]
        public async void NotRunIfInputValidatinFails()
        {
            using (var container = new Container(this))
            {
                container.Register<ExecutionContext>(new LocalExecutionContext());

                var mock = new Mock<IInputValidationRule<TestCommand>>();
                mock.Setup(e => e.Validate(It.IsAny<TestCommand>(), It.IsAny<ExecutionContext>()))
                    .Returns(new[] { new ValidationError("none") });
                container.Register(mock.Object);

                var bus = container.Resolve<IMessageBus>();

                var result = await bus.Send(new TestCommand(null));

                result.ValidationErrors.Count(e => e.ErrorType == ValidationErrorType.Business).ShouldBe(0);
            }
        }

        [Fact]
        public async void NotRunIfSecurityValidatinFails()
        {
            using (var container = new Container(this))
            {
                container.Register<ExecutionContext>(new LocalExecutionContext());

                var mock = new Mock<ISecurityValidationRule<TestCommand>>();
                mock.Setup(e => e.Validate(It.IsAny<TestCommand>(), It.IsAny<ExecutionContext>()))
                    .Returns(Task.FromResult(new ValidationError("none")));
                container.Register(mock.Object);

                var bus = container.Resolve<IMessageBus>();

                var result = await bus.Send(new TestCommand(null));

                result.ValidationErrors.Count(e => e.ErrorType == ValidationErrorType.Business).ShouldBe(0);
            }
        }
    }
}