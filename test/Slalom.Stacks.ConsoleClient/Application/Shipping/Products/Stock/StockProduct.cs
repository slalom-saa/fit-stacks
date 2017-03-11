using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.ConsoleClient.Domain.Shipping;
using Slalom.Stacks.Services;
using Slalom.Stacks.Services.Registry;

namespace Slalom.Stacks.ConsoleClient.Application.Shipping.Products.Stock
{
    [EndPoint("shipping/products/stock")]
    public class StockProduct : UseCase<StockProductCommand, StockProductEvent>
    {
        public override async Task<StockProductEvent> ExecuteAsync(StockProductCommand command)
        {
            var target = (await this.Domain.Find<StockItem>(e => e.ProductId == command.ProductId)).FirstOrDefault();
            if (target == null)
            {
                target = new StockItem(command.ProductId);
            }
            target.Add(command.Quantity);

            await this.Domain.Update(target);

            return new StockProductEvent(target.ProductId, target.Quantity);
        }
    }
}