using System;
using System.Threading.Tasks;
using Slalom.Stacks.ConsoleClient.Application.Products.Stock;
using Slalom.Stacks.ConsoleClient.Domain.Products;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Messaging.Exceptions;

namespace Slalom.Stacks.ConsoleClient.Application.Products.Add
{
    [Path("products/add2")]
    public class AddProduct3 : UseCase<AddProductCommand_v2>
    {
        public override void Execute(AddProductCommand_v2 command)
        {
            Console.WriteLine("xxxxx");
        }
    }

    /// <summary>
    /// Adds a product.  Yay.
    /// </summary>
    [Path("products/add")]
    public class AddProduct : UseCase<AddProductCommand, AddProductEvent>
    {
        public override async Task<AddProductEvent> ExecuteAsync(AddProductCommand command)
        {
            var target = new Product("name");

            await this.Domain.Add(target);

            var stock = await this.Send(new StockProductCommand(command.Count));
            if (!stock.IsSuccessful)
            {
                await this.Domain.Remove(target);

                throw new ChainFailedException(command, stock);
            }

            return new AddProductEvent();
        }
    }

    /// <summary>
    /// Adds a product.  Yay.  Version 2.
    /// </summary>
    [Path("products/add", Version = 2)]
    public class AddProduct_v2 : UseCase<AddProductCommand, AddProductEvent>
    {
        public override async Task<AddProductEvent> ExecuteAsync(AddProductCommand command)
        {
            var target = new Product("name");

            await this.Domain.Add(target);

            var stock = await this.Send(new StockProductCommand(command.Count));
            if (!stock.IsSuccessful)
            {
                await this.Domain.Remove(target);

                throw new ChainFailedException(command, stock);
            }

            return new AddProductEvent();
        }
    }
}