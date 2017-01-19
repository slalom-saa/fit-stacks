using System;
using System.Collections.Generic;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Validation;

namespace WebApplication1.Domain
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
            if (String.IsNullOrWhiteSpace(this.Value))
            {
                yield return "A product must have a valid name.";
            }
        }
    }
}