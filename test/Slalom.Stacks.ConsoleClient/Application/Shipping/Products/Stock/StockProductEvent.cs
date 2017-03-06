using Slalom.Stacks.Messaging;

namespace Slalom.Stacks.ConsoleClient.Application.Shipping.Products.Stock
{
    public class StockProductEvent : EventData
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