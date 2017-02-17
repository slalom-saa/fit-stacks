using System;
using FluentAssertions;
using System.Linq;
using Slalom.Stacks.TestStack;
using Slalom.Stacks.TestStack.Examples.Actors.Items.Add;
using Slalom.Stacks.TestStack.Examples.Domain;
using Slalom.Stacks.TestStack.Examples.Search;
using Slalom.Stacks.UnitTests;
using Xunit;

namespace Slalom.Stacks.UnitTests
{
    public class Scenarios
    {
        public static Scenario StateOne => new StateOneScenario();

        public static Scenario StateZero => new Scenario().AsAdmin();

        public static Scenario Get(string name)
        {
            if (name == "StateOne")
            {
                return StateOne;
            }
            if (name == "StateZero")
            {
                return StateZero;
            }
            return new Scenario();
        }
    }

    public class StateOneScenario : Scenario
    {
        public StateOneScenario()
        {
            this.WithData(Item.Create("first")).AsAdmin();
        }
    }
}

public class SaveItemShould
{
    [Fact, Given(typeof(StateOneScenario))]
    public void A()
    {
        using (var container = new UnitTestContainer(this))
        {
            var result = container.Send(new AddItemCommand("adsf"));

            result.IsSuccessful.Should().BeTrue("The use case execution was not successful.");

            var target = container.Domain.FindAsync<Item>(e => e.Name == "adsf").Result;

            target.Count().Should().Be(1);

            container.RaisedEvents.Count.Should().Be(1);
        }
    }

    [Fact]
    public void A2()
    {
        using (var container = new UnitTestContainer(this))
        {
            var result = container.Send(new AddItemCommand(null));

            result.IsSuccessful.Should().BeFalse();

            result.ValidationErrors.Should().Contain(e => e.Message.Contains("name"));
        }
    }

    [Fact]
    public void A3()
    {
        using (var container = new UnitTestContainer(this))
        {
            var result = container.Send(new AddItemCommand(""));

            result.IsSuccessful.Should().BeFalse();

            result.ValidationErrors.Should().Contain(e => e.Message.Contains("name"));
        }
    }

    [Fact, Given(typeof(StateOneScenario))]
    public void A4()
    {
        using (var container = new UnitTestContainer(this))
        {
            var result = container.Send(new AddItemCommand("ssss"));

            result.IsSuccessful.Should().BeTrue();

            container.Search.Search<ItemSearchResult>().Should().NotBeEmpty();
        }
    }

    [Fact]
    public void A5()
    {
        using (var container = new UnitTestContainer(this))
        {
            var result = container.Send(new AddItemCommand("first"));

            result.IsSuccessful.Should().BeFalse();
        }
    }
}