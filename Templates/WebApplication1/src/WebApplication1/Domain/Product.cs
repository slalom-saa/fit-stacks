using Slalom.Stacks.Domain;

namespace WebApplication1.Domain
{
    public class Product : Entity, IAggregateRoot
    {
        public string Description { get; set; }

        public bool Expired { get; set; }

        public ProductName Name { get; set; }

        public static Product Create(string name, string description)
        {
            return new Product
            {
                Name = name,
                Description = description
            };
        }

        public void Expire()
        {
            this.Expired = true;
        }

        public void Update(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }
    }
}