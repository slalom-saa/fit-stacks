using Slalom.Stacks.Messaging;
using WebApplication1.Controllers;
using WebApplication1.Domain;

namespace WebApplication1.Actors.Products.Add
{
    [Event(1000, Name = "Product Added")]
    public class AddProductEvent : Event
    {
        public Product Product { get; }

        public AddProductEvent(Product product)
        {
            this.Product = product;
        }
    }
}