using Slalom.Stacks.Messaging;

namespace WebApplication1.Actors.Products.Activate
{
    public class ActivateProductEvent : Event
    {
        public string ProductId { get; }

        public ActivateProductEvent(string productId)
        {
            this.ProductId = productId;
        }
    }
}