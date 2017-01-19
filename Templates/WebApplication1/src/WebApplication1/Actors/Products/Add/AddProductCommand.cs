using Slalom.Stacks.Messaging;

namespace WebApplication1.Actors.Products.Add
{
    public class AddProductCommand : Command
    {
        public string Name { get; }

        public string Description { get; }

        public AddProductCommand(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }
    }
}