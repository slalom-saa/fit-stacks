using System.Threading.Tasks;
using Slalom.Stacks.Messaging.Validation;
using Slalom.Stacks.Validation;
using WebApplication1.Domain;

namespace WebApplication1.Actors.Products.Activate.Rules
{
    public class product_must_exist : BusinessRule<ActivateProductCommand>
    {
        public override async Task<ValidationError> ValidateAsync(ActivateProductCommand instance)
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