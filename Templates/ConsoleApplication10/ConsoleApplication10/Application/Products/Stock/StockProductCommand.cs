using Slalom.Stacks.Messaging;

namespace ConsoleApplication10.Application.Products.Stock
{
    public class StockProductCommand : Command
    {
        public int ItemCount { get; }

        public StockProductCommand(int itemCount)
        {
            this.ItemCount = itemCount;
        }
    }
}