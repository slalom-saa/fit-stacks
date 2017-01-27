using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Test.Examples.Domain;

namespace Slalom.Stacks.Test
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
