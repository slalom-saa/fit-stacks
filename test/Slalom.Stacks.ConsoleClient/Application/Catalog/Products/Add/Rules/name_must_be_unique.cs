using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.ConsoleClient.Domain.Products;
using Slalom.Stacks.Messaging.Validation;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.ConsoleClient.Application.Products.Add.Rules
{
    public class name_must_be_unique : BusinessRule<AddProductCommand>
    {
        public override async Task<ValidationError> ValidateAsync(AddProductCommand instance)
        {
            if (await this.Domain.Exists<Product>(e => e.Name.Value == instance.Name))
            {
                return "A product name must be unique.";
            }
            return null;
        }
    }
}
