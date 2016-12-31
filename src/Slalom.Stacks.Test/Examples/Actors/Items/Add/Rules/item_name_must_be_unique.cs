using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Messaging.Validation;
using Slalom.Stacks.Test.Examples.Domain;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Test.Examples.Actors.Items.Add.Rules
{
    public class item_name_must_be_unique : BusinessRule<AddItemCommand>
    {
        protected override IEnumerable<ValidationError> Validate(AddItemCommand instance)
        {
            var target = this.Domain.FindAsync<Item>(e => e.Text == instance.Text).Result.ToList();
            if (target.Any())
            {
                yield return "The item name must be unique.";
            }
            yield break;
        }
    }
}
