using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Shouldly;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Test;
using Slalom.Stacks.Test.Examples.Actors.Items.Add;
using Slalom.Stacks.Test.Examples.Domain;
using Slalom.Stacks.Test.Examples.Search;
using Xunit;

namespace Slalom.Stacks.UnitTests
{


    //public class Scenario
    //{
    //    public class ItemEntityContext : InMemoryEntityContext
    //    {
    //        public ItemEntityContext()
    //        {
    //            this.Instances.Add(Item.Create("A"));
    //        }
    //    }
    //}

    public class Scenarios
    {
        public static Scenario StateZero => new Scenario();
    }


    //public class StateZeroDataScenario : Scenario
    //{
    //    public static Scenario AsAdmin()
    //    {
    //        var scenario = new StateZeroDataScenario();
    //        scenario.WithUser("user", "Administrator")
    //            .WithData(
    //        return scenario;
    //    }
    //}

    public class ScenarioAttribute : Attribute
    {
        public string Scenario { get; }

        public ScenarioAttribute(string scenario)
        {
            this.Scenario = scenario;
        }
    }

    public class SaveItemShould
    {
        [Fact, Scenario("adsf")]
        public async Task A()
        {
            using (var container = new UnitTestContainer(this))
            {
                container.UseScenario(Scenarios.StateZero.AsAdmin());

                var result = await container.Commands.SendAsync(new AddItemCommand("adsf"));

                result.IsSuccessful.ShouldBeTrue("The use case execution was not successful.");

                var target = await container.Domain.FindAsync<Item>(e => e.Text == "adsf");

                target.Count().ShouldBe(1);

                container.RaisedEvents.Count.ShouldBe(1);
            }
        }

        [Fact]
        public async Task A2()
        {
            using (var container = new UnitTestContainer(this))
            {
                container.UseScenario(Scenarios.StateZero.AsAdmin());

                var result = await container.Commands.SendAsync(new AddItemCommand(null));

                result.IsSuccessful.ShouldBeFalse();

                result.ValidationErrors.ShouldContain(e => e.Message.Contains("Text"));
            }
        }

        [Fact]
        public async Task A3()
        {
            using (var container = new UnitTestContainer(this))
            {
                var result = await container.Commands.SendAsync(new AddItemCommand(""));

                result.IsSuccessful.ShouldBeFalse();

                result.ValidationErrors.ShouldContain(e => e.Message.Contains("Text"));
            }
        }

        [Fact]
        public async Task A4()
        {
            using (var container = new UnitTestContainer(this))
            {
                var result = await container.Commands.SendAsync(new AddItemCommand("ss"));

                result.IsSuccessful.ShouldBeTrue();

                container.Search.Search<ItemSearchResult>().Count().ShouldBe(1);
            }
        }

        [Fact]
        public async Task A5()
        {
            using (var container = new UnitTestContainer(this))
            {
                await container.Domain.AddAsync(Item.Create("A"));

                var result = await container.Commands.SendAsync(new AddItemCommand("A"));

                result.IsSuccessful.ShouldBeFalse();
            }
        }
    }
}
