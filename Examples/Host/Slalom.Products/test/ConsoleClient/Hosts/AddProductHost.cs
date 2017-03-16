using Slalom.Products.Application.Catalog.Products.Add;
using Slalom.Stacks.Messaging.Routing;
using Slalom.Stacks.Services.Registry;

namespace ConsoleClient
{
    [EndPoint("catalog/products/add")]
    public class AddProductHost : EndPointHost<AddProduct>
    {
        public AddProductHost(AddProduct service)
            : base(service)
        {
        }

        public override int Retries => 3;
    }
}