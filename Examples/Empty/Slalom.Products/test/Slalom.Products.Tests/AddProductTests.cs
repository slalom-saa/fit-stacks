using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Products.Application.Catalog.Products.Add;
using Slalom.Stacks.TestKit;
using Xunit;

namespace Slalom.Products.Tests
{
    public class AddProductTests
    {
        [Fact]
        public void ShouldBeSuccessful()
        {
            using (var stack = new TestStack(typeof(AddProduct)))
            {
                stack.Send(new AddProductCommand("asdf"));

                stack.LastResult.ShouldBeSuccessful();
            }
        }
    }
}
