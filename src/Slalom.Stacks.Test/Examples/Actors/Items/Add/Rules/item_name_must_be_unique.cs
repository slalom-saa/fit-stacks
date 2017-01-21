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
        public override async Task<ValidationError> ValidateAsync(AddItemCommand instance)
        {
            var target = await this.Domain.FindAsync<Item>(e => e.Text == instance.Text);
            if (target.Any())
            {
                return "The item name must be unique.";
            }
            return null;
        }
    }
}
