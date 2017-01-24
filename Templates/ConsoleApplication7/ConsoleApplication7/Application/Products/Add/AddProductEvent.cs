using ConsoleApplication7.Domain;
using Slalom.Stacks.Messaging;

namespace ConsoleApplication7.Application.Actors.Products.Add
{
    [Event(2000, Name = "Product Added")]
    public class AddProductEvent : Event
    {
        public Product Product { get; }

        public AddProductEvent(Product product)
        {
            this.Product = product;
        }
    }
}