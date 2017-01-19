using Slalom.Stacks.Messaging;
using WebApplication1.Controllers;
using WebApplication1.Domain;

namespace WebApplication1.Actors.Products.Update
{
    public class UpdateProductEvent : Event
    {
        public Product Product { get; }

        public UpdateProductEvent(Product product)
        {
            this.Product = product;
        }
    }
}