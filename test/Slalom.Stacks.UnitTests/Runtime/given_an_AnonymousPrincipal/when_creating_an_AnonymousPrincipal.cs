using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Slalom.Stacks.Runtime;
using Xunit;

namespace Slalom.Stacks.UnitTests.Runtime.given_an_AnonymousPrincipal
{
    public class when_creating_an_AnonymousPrincipal
    {
        [Fact]
        public void Username_should_return_null_string()
        {
            var target = new AnonymousPrincipal();

            target.Identity.Name.Should().BeNull();
        }

        [Fact]
        public void IsAuthenticated_should_be_false()
        {
            var target = new AnonymousPrincipal();

            target.Identity.IsAuthenticated.Should().Be(false);
        }

        [Fact]
        public void IsInRole_should_return_false()
        {
            var target = new AnonymousPrincipal();

            target.IsInRole("Role").Should().BeFalse();
        }
    }
}
