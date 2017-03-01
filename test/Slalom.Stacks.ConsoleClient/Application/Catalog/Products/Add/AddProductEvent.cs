using Slalom.Stacks.Messaging;

namespace Slalom.Stacks.ConsoleClient.Application.Products.Add
{
    /// <summary>
    /// Event that is raised when a product is added to the product catalog.
    /// </summary>
    /// <seealso cref="Slalom.Stacks.Messaging.Event" />
    public class AddProductEvent : Event
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddProductEvent"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        public AddProductEvent(string name, string description)
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