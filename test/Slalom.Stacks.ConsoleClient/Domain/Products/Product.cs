using Slalom.Stacks.Domain;

namespace Slalom.Stacks.ConsoleClient.Domain.Products
{
    public class Product : AggregateRoot
    {
        public ProductName Name { get; set; }

        public ProductDescription Description { get; set; }

        public Product(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }
    }
}