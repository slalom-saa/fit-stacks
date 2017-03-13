using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Slalom.Stacks.Configuration.Actors;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Services;
using Slalom.Stacks.Services.Registry;
using Xunit;
using Service = Slalom.Stacks.Services.Service;

namespace Slalom.Stacks.UnitTests.Services.Registry
{
    public class EndPointTests
    {
        public class EndPointService : Service, IEndPoint<string>
        {
            public Task Receive(string message)
            {
                return Task.FromResult(0);
            }
        }

        [Fact]
        public void GetEndPointsShouldReturnAllEndPoints()
        {
            var target = EndPointMetaData.Create(typeof(EndPointService));

            target.Count().Should().Be(1);
        }
    }
}
