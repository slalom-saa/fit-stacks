using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Messaging;

namespace Slalom.Products.Application.Catalog.Interfaces
{
    [Command("shipping/products/stock")]
    public class StockProductCommand : Command
    {
    }
}
