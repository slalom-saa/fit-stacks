using Slalom.Stacks.Messaging;

namespace Slalom.Products.Application.Catalog.Integration
{
    /// <summary>
    /// Requests that stock be added for a given product.
    /// </summary>
    [Command("shipping/products/stock")]
    public class StockProductCommand : Command
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StockProductCommand" /> class.
        /// </summary>
        /// <param name="productId">The identifier of the product to stock.</param>
        /// <param name="quantity">The quantity to stock.</param>
        public StockProductCommand(string productId, int quantity)
        {
            this.ProductId = productId;
            this.Quantity = quantity;
        }

        /// <summary>
        /// Gets the product identifier.
        /// </summary>
        /// <value>The identifier of the product to stock.</value>
        public string ProductId { get; }

        /// <summary>
        /// Gets the quantity.
        /// </summary>
        /// <value>The quantity to stock.</value>
        public int Quantity { get; }
    }
}