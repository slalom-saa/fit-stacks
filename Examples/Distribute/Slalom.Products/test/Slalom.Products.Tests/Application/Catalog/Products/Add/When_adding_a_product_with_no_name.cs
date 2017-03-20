using Slalom.Products.Application.Catalog.Products.Add;
using Slalom.Stacks.TestKit;
using Xunit;

namespace Slalom.Products.Tests.Application.Catalog.Products.Add
{
    public class When_adding_a_product_with_no_name
    {
        [Fact]
        public void should_not_be_successful()
        {
            using (var stack = new TestStack(typeof(AddProduct)))
            {
                stack.Send(new AddProductCommand(null));

                stack.LastResult.ShouldNotBeSuccessful();
            }
        }

        [Fact]
        public void should_have_the_correct_message()
        {
            using (var stack = new TestStack(typeof(AddProduct)))
            {
                stack.Send(new AddProductCommand(null));

                stack.LastResult.ShouldContainMessage("Name cannot be null.");
            }
        }
    }
}