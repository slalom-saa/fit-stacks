using Slalom.Stacks.Messaging;
using Slalom.Stacks.Validation;

namespace ConsoleApplication7.Application.Actors.Products.Add
{
    [Request("Add Product")]
    public class AddProductCommand : Command
    {
        public AddProductCommand(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }

        [NotNullOrWhiteSpace("The product name must be specified.")]
        public string Name { get; private set; }

        public string Description { get; private set; }
    }
}