using System.Collections.Generic;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.ConsoleClient.Domain.Products
{
    public class ProductName : ConceptAs<string>
    {
        public static implicit operator ProductName(string value)
        {
            var target = new ProductName { Value = value };
            target.EnsureValid();
            return target;
        }

        public override IEnumerable<ValidationError> Validate()
        {
            if (this.Value?.Length < 3 || this.Value?.Length > 100)
            {
                yield return "A product name must be between 3 and 100 characters.";
            }
        }
    }
}