using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Validation;

namespace ConsoleApplication7.Domain
{
    public class ProductName : ConceptAs<string>
    {
        public static implicit operator ProductName(string value)
        {
            var target = new ProductName
            {
                Value = value
            };
            target.EnsureValid();
            return target;
        }

        public override IEnumerable<ValidationError> Validate()
        {
            if (String.IsNullOrWhiteSpace(this.Value))
            {
                yield return "A product name must have a value.";
            }
            if (this.Value.Length < 3)
            {
                yield return "A product name must be greater than 3 characters.";
            }
        }
    }
}
