using System;
using FluentAssertions;
using Slalom.Stacks.Security;
using Slalom.Stacks.Tests;
using Xunit;

namespace Slalom.Stacks.UnitTests.Security
{
    [TestSubject(typeof(AnonymousPrincipal))]
    public class When_creating_an_anonymous_principal
    {
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

        [Fact]
        public void Username_should_return_null_string()
        {
            var target = new AnonymousPrincipal();

            target.Identity.Name.Should().BeNull();
        }
    }
}