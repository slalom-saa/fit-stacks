using System.Collections.Generic;
using System.Threading.Tasks;
using Slalom.Stacks.Messaging.Validation;
using Slalom.Stacks.Validation;
using WebApplication1.Controllers;
using WebApplication1.Domain;

namespace WebApplication1.Actors.Products.Update.Rules
{
    public class product_must_exist : BusinessRule<UpdateProductCommand>
    {
        public override async Task<ValidationError> ValidateAsync(UpdateProductCommand instance)
        {
            var target = await this.Domain.FindAsync<Product>(instance.Id);
            if (target == null)
            {
                return "The specified product must exist.";
            }
            return null;
        }
    }
}