using Slalom.Stacks.Messaging;

namespace ConsoleApplication7.Application.Actors.Products.Activate
{
    public class ActivateProductCommand : Command
    {
        public ActivateProductCommand(string productId)
        {
            this.ProductId = productId;
        }

        public string ProductId { get; private set; }
    }
}