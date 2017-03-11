using System;
using FluentAssertions;
using System.Linq;
using Slalom.Stacks.ConsoleClient.Application.Catalog.Products.Add;
using Slalom.Stacks.ConsoleClient.Application.Shipping.Products.Stock;
using Slalom.Stacks.ConsoleClient.Domain.Products;
using Slalom.Stacks.ConsoleClient.Domain.Shipping;
using Slalom.Stacks.TestKit;
using Slalom.Stacks.Validation;
using Xunit;

// ReSharper disable ObjectCreationAsStatement

namespace Slalom.Stacks.UnitTests
{
    public class StateZero : Scenario
    {
    }

    public class StateOne : StateZero
    {
        public StateOne()
        {
            this.WithData(new Product("abcde", ""));
        }
    }

    public class StockOne : StateZero
    {
        public static string ProductId = "abcde";

        public StockOne()
        {
            var item = new StockItem(ProductId);
            item.Add(5);
            this.WithData(item);
        }
    }

    public class When_creating_a_product
    {
        [Fact]
        public void ProductNameShouldNotThrowWithFiveCharacters()
        {
            new Action(() => new Product("abcde", "")).ShouldNotThrow<ValidationException>();
        }

        [Fact]
        public void ProductNameShouldThrowWithLessThanThreeCharacters()
        {
            new Action(() => new Product("", "")).ShouldThrow<ValidationException>();
        }

        [Fact]
        public void ProductNameShouldThrowWithMoreThanOneHundredCharacters()
        {
            new Action(() => new Product(new string('a', 101), "")).ShouldThrow<ValidationException>();
        }
    }

    public class When_adding_a_product
    {
        [Fact]
        public void a_product_with_the_name_and_description_are_added()
        {
            using (var context = new TestStack(typeof(AddProduct)))
            {
                context.Send(new AddProductCommand("abcde", ""));

                context.Domain.Find<Product>().Result.Count().Should().Be(1);
            }
        }
    }

    public class When_stocking_a_product_with_invalid_quantity
    {
        [Fact]
        public void a_validation_error_should_be_returned()
        {
            using (var context = new TestStack(typeof(AddProduct)))
            {
                context.Send(new StockProductCommand("asdf", -1));

                context.LastResult.ShouldContainMessage("The product quantity must be greater than 0.");
            }
        }
    }

    public class When_stocking_a_product
    {
        [Fact]
        public void should_add_an_item_if_not_exists()
        {
            using (var context = new TestStack(typeof(AddProduct)))
            {
                context.Send(new StockProductCommand("asdf", 10));

                var target = context.Domain.Find<StockItem>(e => e.ProductId == "asdf").Result.FirstOrDefault();

                target.Should().NotBeNull();
            }
        }

        [Fact]
        public void should_increment_the_quantity()
        {
            using (var context = new TestStack(typeof(AddProduct)))
            {
                context.Send(new StockProductCommand("asdf", 10));

                var target = context.Domain.Find<StockItem>(e => e.ProductId == "asdf").Result.FirstOrDefault();

                target.Quantity.Should().Be(10);
            }
        }

        [Fact, Given(typeof(StockOne))]
        public void should_increment_the_quantity_of_existing_item()
        {
            using (var context = new TestStack(typeof(AddProduct)))
            {
                context.Send(new StockProductCommand(StockOne.ProductId, 10));

                var target = context.Domain.Find<StockItem>(e => e.ProductId == StockOne.ProductId).Result.FirstOrDefault();

                target.Quantity.Should().Be(15);
            }
        }
    }

    public class When_adding_a_product_with_non_unique_name
    {
        [Fact, Given(typeof(StateOne))]
        public void a_validation_error_should_be_returned()
        {
            using (var context = new TestStack(typeof(AddProduct)))
            {
                context.Send(new AddProductCommand("abcde", ""));

                context.LastResult.ShouldContainMessage("A product name must be unique.");
            }
        }
    }
}