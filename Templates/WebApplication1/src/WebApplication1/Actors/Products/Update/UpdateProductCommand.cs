using Slalom.Stacks.Messaging;

namespace WebApplication1.Actors.Products.Update
{
    public class UpdateProductCommand : Command
    {
        public UpdateProductCommand(string id, string name, string description)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
        }

        public string Id { get; }

        public string Name { get; }

        public string Description { get; }
    }
}