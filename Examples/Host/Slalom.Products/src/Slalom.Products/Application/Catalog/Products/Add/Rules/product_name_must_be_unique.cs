using System.Threading.Tasks;
using Slalom.Products.Domain.Catalog.Products;
using Slalom.Stacks.Messaging.Validation;
using Slalom.Stacks.Validation;

namespace Slalom.Products.Application.Catalog.Products.Add.Rules
{
    /// <summary>
    /// Validates that the product to add has a unique name.
    /// </summary>
    public class product_name_must_be_unique : BusinessRule<AddProductCommand>
    {
        /// <inheritdoc />
        public override async Task<ValidationError> ValidateAsync(AddProductCommand instance)
        {
            if (await this.Domain.Exists<Product>(e => e.Name == instance.Name))
            {
                return "A product name must be unique.";
            }
            return null;
        }
    }
}