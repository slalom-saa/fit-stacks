using Slalom.Stacks.Messaging;
using Slalom.Stacks.Messaging.Events;

namespace Slalom.Stacks.ConsoleClient.Application.Catalog.Products.Add
{
    /// <summary>
    /// EventMessage that is raised when a product is added to the product catalog.
    /// </summary>
    public class ProductAdded : Event
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductAdded" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        public ProductAdded(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; }
    }
}