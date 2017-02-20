using System.Threading.Tasks;
using ConsoleApplication10.Application.Products.Stock;
using ConsoleApplication10.Domain.Products;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Messaging.Exceptions;

namespace ConsoleApplication10.Application.Products.Add
{
    [Path("products/add")]
    public class AddProduct : UseCaseActor<AddProductCommand, Product>
    {
        public override async Task<Product> ExecuteAsync(AddProductCommand command)
        {
            var target = new Product("name");

            await this.Domain.AddAsync(target);

            var stock = await this.Send(new StockProductCommand(command.Count));
            if (!stock.IsSuccessful)
            {
                await this.Domain.RemoveAsync(target);

                throw new ChainFailedException(command, stock);
            }

            return target;
        }
    }
}