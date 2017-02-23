using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.ConsoleClient.Application.Products.Stock
{
    public class StockProduct : UseCase<StockProductCommand>
    {
        public override IEnumerable<ValidationError> Validate(StockProductCommand command)
        {
            return base.Validate(command);
        }

        public override void Execute(StockProductCommand message)
        {
        }
    }
}
