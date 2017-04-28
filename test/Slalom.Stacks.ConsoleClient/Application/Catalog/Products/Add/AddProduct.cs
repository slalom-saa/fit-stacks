using System.Threading.Tasks;
using Slalom.Stacks.ConsoleClient.Domain.Products;
using Slalom.Stacks.Services;
using Slalom.Stacks.Services.Messaging;

namespace Slalom.Stacks.ConsoleClient.Application.Catalog.Products.Add
{
    [Request("notifications/send")]
    public class SendNotification
    {
        public string Email { get; }

        public string Message { get; }

        public SendNotification(string email, string message)
        {
            this.Email = email;
            this.Message = message;
        }
    }

    /// <summary>
    /// Adds a product to the product catalog.
    /// </summary>
    [EndPoint("catalog/products/add")]
    public class AddProduct : EndPoint<AddProductCommand>
    {
        /// <inheritdoc />
        public override async Task ReceiveAsync(AddProductCommand command)
        {
            var target = new Product(command.Name, command.Description);

            await this.Domain.Add(target);

            //await this.Send(new SendNotification("adsf", "adf"));

            this.AddRaisedEvent(new ProductAdded(target.Name, target.Description));
        }
    }

    /// <summary>
    /// Adds a product to the product catalog.
    /// </summary>
    [EndPoint("catalog/products/add", Version = 2, Name = "Add Product")]
    public class AddProduct2 : EndPoint<AddProductCommand, ProductAdded>
    {
        /// <inheritdoc />
        public override async Task<ProductAdded> ReceiveAsync(AddProductCommand command)
        {
            var target = new Product(command.Name, command.Description);

            await this.Domain.Add(target);

            //await this.Send(new SendNotification("adsf", "adf"));

            return new ProductAdded(target.Name, target.Description);
        }
    }
}