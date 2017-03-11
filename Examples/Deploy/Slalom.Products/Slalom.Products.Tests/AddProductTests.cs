using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Machine.Specifications;
using Slalom.Products.Application.Catalog.Products.Add;
using Slalom.Stacks.TestKit;

namespace Slalom.Products.Tests
{
    class AddProductTests
    {
        private static TestStack Stack;

        private Establish context = () => Stack = new TestStack();

        private Because of = () => Stack.Send(new AddProductCommand("asdf"));
    }
}
