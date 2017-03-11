using Slalom.Stacks.Domain;
using Slalom.Stacks.Validation;

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

        public ProductState State { get; private set; }

        public void Publish()
        {
            if (this.State != ProductState.Draft)
            {
                throw new ValidationException("A non-draft product cannot be published.");
            }
            this.State = ProductState.Published;
        }
    }

    public enum ProductState
    {
        None,
        Draft,
        Published,
        Retired
    }
}