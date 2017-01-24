using System;
using System.Linq;
using System.Threading.Tasks;
using ConsoleApplication7.Domain;
using Slalom.Stacks.Messaging.Validation;
using Slalom.Stacks.Validation;

namespace ConsoleApplication7.Application.Actors.Products.Activate.Rules
{
    public class product_must_exist : BusinessRule<ActivateProductCommand>
    {
        public override async Task<ValidationError> ValidateAsync(ActivateProductCommand instance)
        {
            var target = await this.Domain.FindAsync<Product>(instance.ProductId);
            if (target == null)
            {
                return "The product must exist to activate it.";
            }
            return null;
        }
    }
}
