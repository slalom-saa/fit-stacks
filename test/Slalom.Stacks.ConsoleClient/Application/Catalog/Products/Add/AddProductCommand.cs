using Slalom.Stacks.Messaging;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.ConsoleClient.Application.Catalog.Products.Add
{
    /// <summary>
    /// Adds a product to the product catalog.
    /// </summary>
    [Command("catalog/products/add")]
    public class AddProductCommand : Command
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddProductCommand" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        public AddProductCommand(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        [NotNull("Name is required.")]
        public string Name { get; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; }
    }
}