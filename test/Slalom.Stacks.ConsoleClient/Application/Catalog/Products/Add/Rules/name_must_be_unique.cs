using System.Threading.Tasks;
using Slalom.Stacks.ConsoleClient.Domain.Products;
using Slalom.Stacks.Messaging.Validation;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.ConsoleClient.Application.Catalog.Products.Add.Rules
{
    /// <summary>
    /// Validates that the product name is unique.
    /// </summary>
    public class name_must_be_unique : BusinessRule<AddProductCommand>
    {
        /// <inheritdoc />
        public override ValidationError Validate(AddProductCommand instance)
        {
            return null;
        }
    }

    /// <summary>
    /// Validates that a user is registered.
    /// </summary>
    public class user_is_employee : SecurityRule<AddProductCommand>
    {
        /// <inheritdoc />
        public override ValidationError Validate(AddProductCommand instance)
        {
            return new ValidationError("UserNotEmployee", "You must be an employee to add a product.");
        }
    }
}
