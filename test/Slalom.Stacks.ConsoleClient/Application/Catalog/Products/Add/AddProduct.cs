using System;
using System.Threading.Tasks;
using Slalom.Stacks.ConsoleClient.Domain.Products;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Messaging.Exceptions;
using Slalom.Stacks.Services;

namespace Slalom.Stacks.ConsoleClient.Application.Products.Add
{
    /// <summary>
    /// Adds a product to the product catalog.
    /// </summary>
    [EndPoint("catalog/products/add")]
    public class AddProduct : UseCase<AddProductCommand>
    {
        /// <inheritdoc />
        public override async Task ExecuteAsync(AddProductCommand command)
        {
            var target = new Product(command.Name, command.Description);

            await this.Domain.Add(target);

            this.AddRaisedEvent(new ProductAdded(target.Name, target.Description));
        }
    }

    /// <summary>
    /// Adds a product to the product catalog.
    /// </summary>
    [EndPoint("catalog/products/add", Version = 2)]
    public class AddProduct2 : UseCase<AddProductCommand>
    {
        /// <inheritdoc />
        public override async Task ExecuteAsync(AddProductCommand command)
        {
            var target = new Product(command.Name, command.Description);

            await this.Domain.Add(target);

            this.AddRaisedEvent(new ProductAdded(target.Name, target.Description));
        }
    }
}