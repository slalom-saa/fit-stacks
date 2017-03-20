using Slalom.Stacks.Messaging.Events;

namespace Slalom.Products.Application.Catalog.Products.Add
{
    /// <summary>
    /// Raised when a product has been added.
    /// </summary>
    public class AddProductEvent : Event
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddProductEvent" /> class.
        /// </summary>
        /// <param name="productId">The identifier of the added product.</param>
        /// <param name="productName">The name of the added product.</param>
        public AddProductEvent(string productId, string productName)
        {
            this.ProductId = productId;
            this.ProductName = productName;
        }

        /// <summary>
        /// Gets the product identifier.
        /// </summary>
        /// <value>The identifier of the added product.</value>
        public string ProductId { get; }

        /// <summary>
        /// Gets the name of the product.
        /// </summary>
        /// <value>The name of the added product.</value>
        public string ProductName { get; }
    }
}