using Slalom.Stacks.Messaging;
using Slalom.Stacks.Messaging.Events;

namespace Slalom.Products.Application.Catalog.Products.Add
{
    public class AddProductEvent : Event
    {
        public string ProductId { get; }

        public string ProductName { get; }

        public AddProductEvent(string productId, string productName)
        {
            this.ProductId = productId;
            this.ProductName = productName;
        }
    }
}