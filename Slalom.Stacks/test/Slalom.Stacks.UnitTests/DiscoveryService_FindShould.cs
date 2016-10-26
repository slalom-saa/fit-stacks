using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Reflection;
using Shouldly;
using Slalom.Stacks.Logging;
using Xunit;

namespace Slalom.Stacks.UnitTests
{
    public class DiscoveryService_FindShould
    {
        public class TestEvent : Event
        {
        }

        [Fact]
        public void FindEvents()
        {
            var service = new DiscoveryService(new NullLogger());

            service.Find<Event>().Count(e => e == typeof(TestEvent)).ShouldBe(1);
        }
    }
}
