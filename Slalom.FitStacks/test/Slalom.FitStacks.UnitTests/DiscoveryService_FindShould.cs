using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.FitStacks.Messaging;
using Slalom.FitStacks.Reflection;
using Shouldly;
using Slalom.FitStacks.Logging;
using Xunit;

namespace Slalom.FitStacks.UnitTests
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
