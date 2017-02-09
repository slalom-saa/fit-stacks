using Slalom.Stacks.Messaging;

namespace ConsoleApplication7.Application.Actors.Products.Get
{
    public class GetProductCommand : Command
    {
        public GetProductCommand(string productId)
        {
            this.ProductId = productId;
        }

        public string ProductId { get; private set; }
    }
}