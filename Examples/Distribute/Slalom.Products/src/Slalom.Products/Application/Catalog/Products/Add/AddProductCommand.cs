using Slalom.Stacks.Messaging;
using Slalom.Stacks.Validation;

namespace Slalom.Products.Application.Catalog.Products.Add
{
    /// <summary>
    /// Adds a product to the product catalog so that a user can search for it and it can be added to a cart, purchased and/or shipped.
    /// </summary>
    public class AddProductCommand : Command
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddProductCommand" /> class.
        /// </summary>
        /// <param name="name">The name of the product to add.</param>
        public AddProductCommand(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name of the product to add.</value>
        [NotNull("Name cannot be null.")]
        public string Name { get; }
    }
}