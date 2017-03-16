using Slalom.Stacks.Domain;

namespace Slalom.Products.Domain.Catalog.Products
{
    /// <summary>
    /// Represents a product.  <see href="[Document URL]" />
    /// </summary>
    public class Product : AggregateRoot
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Product" /> class.
        /// </summary>
        /// <param name="name">The product name.</param>
        /// <param name="description">(Optional) The description.</param>
        public Product(string name, string description = null)
        {
            this.Name = name;
            this.Description = description;
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The product description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The product name.</value>
        public ProductName Name { get; }
    }
}