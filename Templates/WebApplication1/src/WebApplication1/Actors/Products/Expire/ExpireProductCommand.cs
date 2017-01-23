using System;
using System.Linq;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Validation;

namespace WebApplication1.Actors.Products.Expire
{
    public class ExpireProductCommand : Command
    {
        [NotNullOrWhitespace("The product Id cannot be null or whitespace.")]
        public string Id { get; }

        public ExpireProductCommand(string id)
        {
            this.Id = id;
        }
    }
}
