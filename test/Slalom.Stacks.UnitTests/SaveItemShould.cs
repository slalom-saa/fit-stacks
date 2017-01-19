using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shouldly;
using Slalom.Stacks.Test;
using Slalom.Stacks.Test.Examples.Actors.Items.Add;
using Slalom.Stacks.Test.Examples.Domain;
using Slalom.Stacks.Test.Examples.Search;
using Xunit;

namespace Slalom.Stacks.UnitTests
{
    public class SaveItemShould
    {
        [Fact]
        public async Task A()
        {
            using (var container = new UnitTestContainer())
            {
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
            using (var container = new UnitTestContainer())
            {
                var result = await container.Commands.SendAsync(new AddItemCommand(null));

                result.IsSuccessful.ShouldBeFalse();

                result.ValidationErrors.ShouldContain(e => e.Message.Contains("Text"));
            }
        }

        [Fact]
        public async Task A3()
        {
            using (var container = new UnitTestContainer())
            {
                var result = await container.Commands.SendAsync(new AddItemCommand(""));

                result.IsSuccessful.ShouldBeFalse();

                result.ValidationErrors.ShouldContain(e => e.Message.Contains("Text"));
            }
        }

        [Fact]
        public async Task A4()
        {
            using (var container = new UnitTestContainer())
            {
                var result = await container.Commands.SendAsync(new AddItemCommand("ss"));

                result.IsSuccessful.ShouldBeTrue();

                container.Search.Search<ItemSearchResult>().Count().ShouldBe(1);
            }
        }

        [Fact]
        public async Task A5()
        {
            using (var container = new UnitTestContainer())
            {
                await container.Domain.AddAsync(Item.Create("A"));

                var result = await container.Commands.SendAsync(new AddItemCommand("A"));

                result.IsSuccessful.ShouldBeFalse();
            }
        }
    }
}
