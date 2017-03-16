using System.Collections.Generic;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Validation;

namespace Slalom.Products.Domain.Catalog.Products
{
    /// <summary>
    /// Represents a product name.
    /// </summary>
    public class ProductName : ConceptAs<string>
    {
        /// <summary>
        /// Performs an implicit conversion from <see cref="System.String" /> to <see cref="ProductName" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator ProductName(string value)
        {
            var target = new ProductName {Value = value};
            target.EnsureValid();
            return target;
        }

        /// <inheritdoc />
        public override IEnumerable<ValidationError> Validate()
        {
            if (string.IsNullOrWhiteSpace(this.Value) || this.Value.Length < 3)
            {
                yield return "A product name must be greater than 3 characters.";
            }
        }
    }
}