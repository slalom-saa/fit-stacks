using System;
using System.Linq;
using System.Threading.Tasks;
using Shouldly;
using Slalom.Stacks.Communication;
using Slalom.Stacks.Configuration;
using Slalom.Stacks.Runtime;
using Xunit;

namespace Slalom.Stacks.IntegrationTests
{
    public class Container_ResolveShould
    {
        public class NewMessageBus : IMessageBus
        {
            public Task<CommandResult<TResult>> Send<TResult>(Command<TResult> command)
            {
                throw new NotImplementedException();
            }
        }

        [Fact]
        public void FindEventPublisher()
        {
            using (var container = new Container(this))
            {
                container.Resolve<IEventPublisher>().ShouldNotBeNull();
            }
        }

        [Fact]
        public void FindMessageBus()
        {
            using (var container = new Container(this))
            {
                container.Register<LocalExecutionContext>();
                container.Resolve<IMessageBus>().ShouldNotBeNull();
            }
        }

        [Fact]
        public void FindUpdatedType()
        {
            using (var container = new Container(this))
            {
                container.Register<NewMessageBus>();
                container.Register<LocalExecutionContext>();

                container.Resolve<IMessageBus>().ShouldBeOfType<NewMessageBus>();
            }
        }
    }
}