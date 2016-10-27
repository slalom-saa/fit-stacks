using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Xunit;
using Shouldly;
using Moq;
using Slalom.Stacks.Communication;
using Slalom.Stacks.Configuration;
using Slalom.Stacks.Runtime;

namespace Slalom.Stacks.IntegrationTests
{
    public class CommandHandler_HandleShould : IDisposable
    {
        private IMessageBus _bus;
        private Container _container;

        public class TestCommand : Command<string>
        {
            public string Text { get; set; }

            public TestCommand(string text)
            {
                this.Text = text;
            }
        }



        public class TestCommandHandler : CommandHandler<TestCommand, string>
        {
            public override Task<string> Handle(TestCommand command)
            {
                return Task.FromResult(command.Text);
            }
        }

        public CommandHandler_HandleShould()
        {
            _container = new Container(this);

            _container.Register<LocalExecutionContext>();
            _container.Register<TestCommandHandler>();
            _bus = _container.Resolve<IMessageBus>();
        }

        [Theory, InlineData("Hello")]
        public async void ReturnValue(string text)
        {
            var result = await _bus.Send(new TestCommand(text));

            result.Value.ShouldBe(text);
        }

        [Theory, InlineData("Hello")]
        public async void HaveContext(string text)
        {
            ExecutionContext context = null;

            var mock = new Mock<ICommandHandler<TestCommand, string>>();


            mock.SetupSet(e => e.Context = It.IsAny<ExecutionContext>()).Callback<ExecutionContext>(a =>
              {
                  context = a;
              });

            _container.Register(mock.Object);

            var result = await _bus.Send(new TestCommand(text));

            context.ShouldNotBeNull();
        }

        public void Dispose()
        {
            _container.Dispose();
        }
    }
}
