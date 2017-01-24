using Slalom.Stacks.Messaging;

namespace ConsoleApplication7.Application.Actors.Products.Activate
{
    [Event(2000, Name = "Product Activated")]
    public class ActivateProductEvent : Event
    {
        public string ProductId { get; }

        public ActivateProductEvent(string productId)
        {
            this.ProductId = productId;
        }
    }
}