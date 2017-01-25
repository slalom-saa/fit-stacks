using System.ComponentModel.DataAnnotations;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Validation;

namespace WebApplication1.Actors.Products.Add
{
    public class AddProductCommand : Command
    {
        [Range(100, 200, ErrorMessage = "adsf")]
        public string Name { get; }

        public string Description { get; }

        public AddProductCommand(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }
    }
}