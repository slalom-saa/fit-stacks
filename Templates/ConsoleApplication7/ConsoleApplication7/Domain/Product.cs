using System;
using Slalom.Stacks.Domain;

namespace ConsoleApplication7.Domain
{
    public class Product : Entity, IAggregateRoot
    {
        public ProductName Name { get; private set; }

        public string Description { get; private set; }

        public ProductStatus State { get; private set; } = ProductStatus.Draft;

        public Product(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }

        public void Activate()
        {
            if (String.IsNullOrWhiteSpace(this.Description))
            {
                throw new InvalidOperationException("A description is required to activate a product.");
            }
            this.State = ProductStatus.Active;
        }
    }
}