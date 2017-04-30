using Slalom.Stacks.Services;
using Slalom.Stacks.Services.Logging;

namespace Slalom.Stacks.ConsoleClient.Application.Shipping.Products.Stock
{
    public class StockProductEvent : Event
    {
        public StockProductEvent(string productId, int quantity)
        {
            this.ProductId = productId;
            this.Quantity = quantity;
        }

        public string ProductId { get; }

        public int Quantity { get; }
    }
}