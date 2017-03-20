using System.Linq;
using FluentAssertions;
using Slalom.Products.Application.Catalog.Products.Add;
using Slalom.Products.Domain.Catalog.Products;
using Slalom.Stacks.TestKit;
using Xunit;

namespace Slalom.Products.Tests.Application.Catalog.Products.Add
{
    public class When_adding_a_product
    {
        [Fact]
        public void should_be_successful()
        {
            using (var stack = new TestStack(typeof(AddProduct)))
            {
                stack.Send(new AddProductCommand("name"));

                stack.LastResult.ShouldBeSuccessful();
            }
        }

        [Fact]
        public void should_add_a_product()
        {
            using (var stack = new TestStack(typeof(AddProduct)))
            {
                stack.Send(new AddProductCommand("name"));

                stack.Domain.Find<Product>(e => e.Name == "name").Result.Count().Should().Be(1);
            }
        }
    }
}
