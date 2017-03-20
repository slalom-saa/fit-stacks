using Slalom.Stacks.Messaging.Events;

namespace Slalom.Products.Application.Shipping.Products.Stock
{
    /// <summary>
    /// Raised when a request to stock product has been made.
    /// </summary>
    public class StockProductEvent : Event
    {
        /// <summary>
        /// Gets the product identifier.
        /// </summary>
        /// <value>The identifier of the requested product.</value>
        public string ProductId { get; }
        /// <summary>
        /// Gets the quantity.
        /// </summary>
        /// <value>The quantity that was requested.</value>
        public int Quantity { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StockProductEvent"/> class.
        /// </summary>
        /// <param name="productId">The identifier of the requested product.</param>
        /// <param name="quantity">The quantity that was requested.</param>
        public StockProductEvent(string productId, int quantity)
        {
            this.ProductId = productId;
            this.Quantity = quantity;
        }
    }
}