using System.Linq;
using System.Threading.Tasks;
using ConsoleApplication7.Domain;
using Slalom.Stacks.Messaging.Validation;
using Slalom.Stacks.Validation;

namespace ConsoleApplication7.Application.Actors.Products.Add.Rules
{
    public class product_name_must_be_unique : BusinessRule<AddProductCommand>
    {
        public override async Task<ValidationError> ValidateAsync(AddProductCommand instance)
        {
            var target = await this.Domain.FindAsync<Product>(e => e.Name.Value == instance.Name);
            if (target.Any())
            {
                return "A product must have a unique name.";
            }
            return null;
        }
    }
}