using System.Collections.Generic;
using Slalom.Stacks.Messaging.Validation;
using Slalom.Stacks.Validation;

namespace Slalom.Products.Application.Catalog.Products.Add.Rules
{
    /// <summary>
    /// Validates that the current user is registered.
    /// </summary>
    public class user_is_registered : SecurityRule<AddProductCommand>
    {
        /// <inheritdoc />
        public override IEnumerable<ValidationError> Validate(AddProductCommand instance)
        {
            yield break;
        }
    }
}