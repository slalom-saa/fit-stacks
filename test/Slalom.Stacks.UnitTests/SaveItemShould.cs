using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using FluentAssertions;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Test;
using Slalom.Stacks.Test.Examples.Actors.Items.Add;
using Slalom.Stacks.Test.Examples.Domain;
using Slalom.Stacks.Test.Examples.Search;
using Xunit;

namespace Slalom.Stacks.UnitTests
{
    public class Scenarios
    {
        public static Scenario StateZero => new Scenario().AsAdmin();

        public static Scenario StateOne => new StateOneScenario();

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


    

        public ScenarioAttribute(string scenario)
        {
            this.Scenario = scenario;
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

                container.UseScenario(Scenarios.Get(""));

                result.IsSuccessful.Should().BeTrue("The use case execution was not successful.");

                var target = container.Domain.FindAsync<Item>(e => e.Text == "adsf").Result;

                target.Count().Should().Be(1);

                container.RaisedEvents.Count.Should().Be(1);
            }
        }

        [Fact]
        public void A2()
        {
            using (var container = new UnitTestContainer(this))
            {
                container.UseScenario(Scenarios.StateZero.AsAdmin());

                var result = container.Send(new AddItemCommand(null));

                result.IsSuccessful.Should().BeFalse();

                result.ValidationErrors.Should().Contain(e => e.Message.Contains("text"));
            }
        }

        [Fact]
        public void A3()
        {
            using (var container = new UnitTestContainer(this))
            {
                var result = container.Send(new AddItemCommand(""));

                result.IsSuccessful.Should().BeFalse();

                result.ValidationErrors.Should().Contain(e => e.Message.Contains("text"));
            }
        }

        [Fact]
        public void A4()
        {
            using (var container = new UnitTestContainer(this))
            {
                container.UseScenario(Scenarios.StateZero.AsAdmin());

                var result = container.Send(new AddItemCommand("ss"));

                result.IsSuccessful.Should().BeTrue();

                container.Search.Search<ItemSearchResult>().Count().Should().Be(1);
            }
        }

        [Fact]
        public void A5()
        {
            using (var container = new UnitTestContainer(this))
            {
                container.UseScenario(Scenarios.StateOne.AsAdmin());

                var result = container.Send(new AddItemCommand("first"));

                result.IsSuccessful.Should().BeFalse();
            }
        }
    }
}
