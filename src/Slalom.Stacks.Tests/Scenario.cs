using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Slalom.Stacks.Domain;

namespace Slalom.Stacks.Tests
{
    public class Scenario
    {
        public InMemoryEntityContext EntityContext { get; set; } = new InMemoryEntityContext();

        public Scenario WithData(params IAggregateRoot[] items)
        {
            this.EntityContext.Add(items).Wait();

            return this;
        }
    }
}