using System;
using Slalom.Stacks.TestStack.Examples.Domain;

namespace Slalom.Stacks.TestStack
{
    public class Tests
    {
        public void Do()
        {
            using (var container = new UnitTestContainer(this))
            {
                container.Domain.AddAsync(Item.Create("adf"));
            }
        }
    }
}
