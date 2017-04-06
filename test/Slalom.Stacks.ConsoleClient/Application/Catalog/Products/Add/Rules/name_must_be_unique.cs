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
    public class user_must_be_registered : SecurityRule<AddProductCommand>
    {
        /// <inheritdoc />
        public override ValidationError Validate(AddProductCommand instance)
        {
            // TODO: perform validation here
            if (this.Request.User.Identity.IsAuthenticated)
            {
            }

            return new ValidationError("UserNotRegistered", "You must be registered to submit a product.");
        }
    }
}
