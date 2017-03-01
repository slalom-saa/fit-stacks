using System;
using System.Threading.Tasks;
using Slalom.Stacks.ConsoleClient.Domain.Products;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Messaging.Exceptions;

namespace Slalom.Stacks.ConsoleClient.Application.Products.Add
{
    /// <summary>
    /// Adds a product to the product catalog.
    /// </summary>
    [Path("catalog/products/add")]
    public class AddProduct : UseCase<AddProductCommand, AddProductEvent>
    {
        /// <inheritdoc />
        public override async Task<AddProductEvent> ExecuteAsync(AddProductCommand command)
        {
            var target = new Product(command.Name, command.Description);

            await this.Domain.Add(target);

            return new AddProductEvent(target.Name, target.Description);
        }
    }
}