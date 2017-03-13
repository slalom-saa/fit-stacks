using System.Threading.Tasks;
using Slalom.Stacks.ConsoleClient.Domain.Products;
using Slalom.Stacks.Messaging.Validation;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.ConsoleClient.Application.Catalog.Products.Add.Rules
{
    /// <summary>
    /// Validates that ....
    /// </summary>
    /// <seealso cref="Slalom.Stacks.Messaging.Validation.BusinessRule{Slalom.Stacks.ConsoleClient.Application.Catalog.Products.Add.AddProductCommand}" />
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
