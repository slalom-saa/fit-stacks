using System;
using System.Linq;
using Slalom.Stacks.Messaging;

namespace ConsoleApplication10.Application.Products.Stock
{
    public class StockProduct : UseCaseActor<StockProductCommand>
    {
        public override void Execute(StockProductCommand message)
        {
        }
    }
}
