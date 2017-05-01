using System.Collections.Generic;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Validation;
#pragma warning disable 1591

namespace Slalom.Stacks.ConsoleClient.Domain.Products
{
    public class ProductDescription : ConceptAs<string>
    {
        public static implicit operator ProductDescription(string value)
        {
            var target = new ProductDescription { Value = value };
            target.EnsureValid();
            return target;
        }

        public override IEnumerable<ValidationError> Validate()
        {
            if (this.Value?.Length > 250)
            {
                yield return "A product description must be less than 250 characters.";
            }
        }
    }
}