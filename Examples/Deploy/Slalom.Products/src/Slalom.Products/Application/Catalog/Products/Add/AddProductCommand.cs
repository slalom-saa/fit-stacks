using Slalom.Stacks.Messaging;
using Slalom.Stacks.Validation;

namespace Slalom.Products.Application.Catalog.Products.Add
{
    public class AddProductCommand : Command
    {
        [NotNull("Name cannot be null.")]
        public string Name { get; }

        public AddProductCommand(string name)
        {
            this.Name = name;
        }
    }
}