using Slalom.Stacks.Services;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.ConsoleClient.Application.Shipping.Products.Stock
{
    public class StockProductCommand
    {
        public StockProductCommand(string productId, int quantity)
        {
            this.ProductId = productId;
            this.Quantity = quantity;
        }

        public string ProductId { get; }

        [GreaterThan(0, "The product quantity must be greater than 0.")]
        public int Quantity { get; }
    }
}