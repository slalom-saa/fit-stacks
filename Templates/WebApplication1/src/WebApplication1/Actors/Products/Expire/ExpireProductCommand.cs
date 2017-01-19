using System;
using System.Linq;
using Slalom.Stacks.Messaging;

namespace WebApplication1.Actors.Products.Expire
{
    public class ExpireProductCommand : Command
    {
        public string Id { get; }

        public ExpireProductCommand(string id)
        {
            this.Id = id;
        }
    }
}
