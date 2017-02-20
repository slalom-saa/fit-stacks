using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Messaging.Validation;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.ConsoleClient.Application.Products.Stock.Rules
{
    public class stock_count_must_be_greater_than_zero : BusinessRule<StockProductCommand>
    {
        protected override IEnumerable<ValidationError> Validate(StockProductCommand instance)
        {
            if (instance.ItemCount < 5)
            {
                yield return "The stock count must be in multiples of 5.";
            }
        }
    }
}
