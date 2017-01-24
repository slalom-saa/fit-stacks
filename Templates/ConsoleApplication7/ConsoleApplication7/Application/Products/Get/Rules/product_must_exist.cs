using System;
using System.Linq;
using System.Threading.Tasks;
using ConsoleApplication7.Domain;
using Slalom.Stacks.Messaging.Validation;
using Slalom.Stacks.Validation;

namespace ConsoleApplication7.Application.Actors.Products.Get.Rules
{
    public class product_must_exist : BusinessRule<GetProductCommand>
    {
        public override async Task<ValidationError> ValidateAsync(GetProductCommand instance)
        {
            var target = await this.Domain.FindAsync<Product>(instance.ProductId);
            if (target == null)
            {
                return "The product with the specified ID does not exist.";
            }
            return null;
        }
    }
}