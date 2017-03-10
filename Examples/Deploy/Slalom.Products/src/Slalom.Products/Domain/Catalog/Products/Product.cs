using System;
using System.Linq;
using Slalom.Stacks.Domain;

namespace Slalom.Products.Domain.Catalog.Products
{
    public class Product : AggregateRoot
    {
        public string Name { get; }

        public Product(string name, string description = null)
        {
            this.Name = name;
            this.Description = description;
        }

        public string Description { get; set; }
    }
}
