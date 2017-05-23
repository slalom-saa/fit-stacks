using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Slalom.Stacks.Services.Inventory;
using Slalom.Stacks.Tests;
using Xunit;

namespace Slalom.Stacks.UnitTests.Services.Inventory
{
    [TestSubject(typeof(EndPointPath))]
    public class When_comparing_endpoint_paths
    {
        [Fact]
        public void should_order_system_after_featured()
        {
            var path = new EndPointPath("_system/one");
            var other = new EndPointPath("other/path");

            path.CompareTo(other).Should().Be(1);

            other.CompareTo(path).Should().Be(-1);
        }

        [Fact]
        public void should_order_system_versioned_after_not_versioned()
        {
            var path = new EndPointPath("/_system/requests");
            var other = new EndPointPath("/v1/_system/health");

            path.CompareTo(other).Should().Be(-1);

            other.CompareTo(path).Should().Be(1);
        }

        [Fact]
        public void should_order_system_versioned_after_other_versioned()
        {
            var path = new EndPointPath("v1/_system/one");
            var other = new EndPointPath("v2/_system/one");

            path.CompareTo(other).Should().Be(-1);

            other.CompareTo(path).Should().Be(1);
        }

        [Fact]
        public void should_order_feature_versioned_after_other_versioned()
        {
            var path = new EndPointPath("v1/feature/one");
            var other = new EndPointPath("v2/feature/one");

            path.CompareTo(other).Should().Be(-1);

            other.CompareTo(path).Should().Be(1);
        }

        [Fact]
        public void should_order_feature_versioned_after_other_versioned_two_digits()
        {
            var path = new EndPointPath("v10/feature/one");
            var other = new EndPointPath("v1/feature/one");

            path.CompareTo(other).Should().Be(1);

            other.CompareTo(path).Should().Be(-1);
        }

        [Fact]
        public void should_order_feature_versioned_after_non_versioned()
        {
            var path = new EndPointPath("feature/one");
            var other = new EndPointPath("v1/feature/one");

            path.CompareTo(other).Should().Be(-1);

            other.CompareTo(path).Should().Be(1);
        }
    }
}
