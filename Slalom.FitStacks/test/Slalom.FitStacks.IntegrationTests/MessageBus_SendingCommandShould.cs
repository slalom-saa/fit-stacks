using System;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Slalom.FitStacks.Configuration;
using Slalom.FitStacks.Messaging;
using Slalom.FitStacks.Runtime;
using Xunit;

namespace Slalom.FitStacks.IntegrationTests
{
    public class MessageBus_SendingCommandShould
    {
        public class TestEvent : Event
        {
            public TestEvent(string text)
            {
                this.Text = text;
            }

            public string Text { get; private set; }
        }

        public class TestCommand : Command<TestEvent>
        {
            public string Text { get; private set; }

            public TestCommand(string text)
            {
                this.Text = text;
            }
        }

        public class AdditionalEvent : Event
        {
        }

        public class TestCommandHandler : CommandHandler<TestCommand, TestEvent>
        {
            public override Task<TestEvent> Handle(TestCommand command)
            {
                this.Context.AddRaisedEvent(new AdditionalEvent());

                return Task.FromResult(new TestEvent(command.Text));
            }
        }

        [Theory, InlineData("test")]
        public async void RaiseAdditionalEvents(string text)
        {
            using (var container = new Container(this))
            {
                var mock = new Mock<IHandleEvent<TestEvent>>();
                mock.Setup(e => e.Handle(It.IsAny<TestEvent>(), It.IsAny<ExecutionContext>()))
                    .Returns(Task.FromResult(0));
                container.Register(mock.Object);

                var additional = new Mock<IHandleEvent<AdditionalEvent>>();
                additional.Setup(e => e.Handle(It.IsAny<AdditionalEvent>(), It.IsAny<ExecutionContext>()))
                    .Returns(Task.FromResult(0));
                container.Register(additional.Object);

                container.Register<LocalExecutionContext>();

                await container.Resolve<IMessageBus>().Send(new TestCommand(text));

                additional.Verify(e => e.Handle(It.IsAny<AdditionalEvent>(), It.IsAny<ExecutionContext>()));
            }
        }

        [Theory, InlineData("test")]
        public async void RaiseEventWithReturnValue(string text)
        {
            using (var container = new Container(this))
            {
                var mock = new Mock<IHandleEvent<TestEvent>>();
                mock.Setup(e => e.Handle(It.IsAny<TestEvent>(), It.IsAny<ExecutionContext>()))
                    .Returns(Task.FromResult(0));
                container.Register(mock.Object);

                container.Register<LocalExecutionContext>();

                await container.Resolve<IMessageBus>().Send(new TestCommand(text));
            }
        }

        [Theory, InlineData("test")]
        public async void RaiseAddedEvents(string text)
        {
            using (var container = new Container(this))
            {
                var mock = new Mock<IHandleEvent<TestEvent>>();
                mock.Setup<Task>(e => e.Handle(It.IsAny<TestEvent>(), It.IsAny<ExecutionContext>()))
                    .Returns(Task.FromResult(0));

                container.Register(mock.Object);
                container.Register<LocalExecutionContext>();

                await container.Resolve<IMessageBus>().Send(new TestCommand(text));
            }
        }

        [Theory, InlineData("test")]
        public async void ContainContext(string text)
        {
            using (var container = new Container(this))
            {
                ExecutionContext context = null;
                var currentContext = new LocalExecutionContext();
                container.Register<ExecutionContext>(currentContext);

                var mock = new Mock<IHandleEvent<TestEvent>>();
                mock.Setup(e => e.Handle(It.IsAny<TestEvent>(), It.IsAny<ExecutionContext>()))
                    .Callback<TestEvent, ExecutionContext>((a, b) =>
                    {
                        context = b;
                    })
                    .Returns(Task.FromResult(0));

                container.Register(mock.Object);

                await container.Resolve<IMessageBus>().Send(new TestCommand(text));

                context.SessionId.ShouldBe(currentContext.SessionId);
            }
        }
    }
}
