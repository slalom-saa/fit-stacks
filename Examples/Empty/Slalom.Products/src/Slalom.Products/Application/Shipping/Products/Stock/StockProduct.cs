using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Services;
using Slalom.Stacks.Services.Registry;

namespace Slalom.Products.Application.Shipping.Products.Stock
{
    public class StockProductCommand : Command
    {
    }

    [EndPoint("shipping/products/stock")]
    public class StockProduct : UseCase<StockProductCommand>
    {
        public override void Execute(StockProductCommand command)
        {
        }
    }
}
