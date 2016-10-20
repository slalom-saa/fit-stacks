using System;
using Shouldly;
using System.Collections;
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
    public class EventPublisher_PublishShould
    {
        public class TestEvent : Event
        {
        }

        public class TestEventHandler1 : IHandleEvent<TestEvent>
        {
            public Task Handle(TestEvent instance, ExecutionContext context)
            {
                return Task.FromResult(0);
            }
        }

        public class TestEventHandler2 : IHandleEvent<TestEvent>
        {
            public Task Handle(TestEvent instance, ExecutionContext context)
            {
                return Task.FromResult(0);
            }
        }

        [Fact]
        public void ResolveAllHandlers()
        {
            using (var container = new Container(this))
            {
                var target = container.Resolve<IEnumerable<IHandleEvent<TestEvent>>>();

                target.Count().ShouldBe(2);
            }
        }

        [Fact]
        public async void FindAllEventHandlers()
        {
            using (var container = new Container(this))
            {
                container.Register<TestEventHandler2>();


                var handler = container.Resolve<IEventPublisher>();

                await handler.Publish(new TestEvent(), new LocalExecutionContext());
            }
        }

        [Fact]
        public async void CallHandlers()
        {
            using (var container = new Container(this))
            {
                var mock = new Mock<IHandleEvent<TestEvent>>();
                container.Register(mock.Object);
                mock.Setup(e => e.Handle(It.IsAny<TestEvent>(), It.IsAny<ExecutionContext>())).Returns(Task.FromResult(0));

                var handler = container.Resolve<IEventPublisher>();

                await handler.Publish(new TestEvent(), new LocalExecutionContext());

                mock.Verify();
            }
        }
    }
}
