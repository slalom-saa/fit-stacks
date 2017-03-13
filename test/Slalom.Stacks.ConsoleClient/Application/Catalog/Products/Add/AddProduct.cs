using System.Threading.Tasks;
using Slalom.Stacks.ConsoleClient.Domain.Products;
using Slalom.Stacks.Services;
using Slalom.Stacks.Services.Registry;

namespace Slalom.Stacks.ConsoleClient.Application.Catalog.Products.Add
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
    [EndPoint("catalog/products/add", Version = 2, Name = "Add Product")]
    public class AddProduct2 : UseCase<AddProductCommand, ProductAdded>
    {
        /// <inheritdoc />
        public override async Task<ProductAdded> ExecuteAsync(AddProductCommand command)
        {
            var target = new Product(command.Name, command.Description);

            await this.Domain.Add(target);

            return new ProductAdded(target.Name, target.Description);
        }
    }
}