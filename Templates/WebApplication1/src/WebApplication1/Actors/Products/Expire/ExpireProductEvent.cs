using Slalom.Stacks.Messaging;

namespace WebApplication1.Actors.Products.Expire
{
    public class ExpireProductEvent : Event
    {
        public string ProductId { get; }

        public ExpireProductEvent(string productId)
        {
            this.ProductId = productId;
        }
    }
}