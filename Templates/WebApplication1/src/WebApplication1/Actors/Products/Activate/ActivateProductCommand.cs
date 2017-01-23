using Slalom.Stacks.Messaging;
using Slalom.Stacks.Validation;

namespace WebApplication1.Actors.Products.Activate
{
    public class ActivateProductCommand : Command
    {
        public string Id { get; }

        public ActivateProductCommand(string id)
        {
            this.Id = id;
        }
    }
}
