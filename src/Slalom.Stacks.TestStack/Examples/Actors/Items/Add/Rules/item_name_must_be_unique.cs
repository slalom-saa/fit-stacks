using System;
using System.Threading.Tasks;
using Slalom.Stacks.Messaging.Validation;
using Slalom.Stacks.TestStack.Examples.Domain;
using Slalom.Stacks.Validation;
using System.Linq;

namespace Slalom.Stacks.TestStack.Examples.Actors.Items.Add.Rules
{
    public class item_name_must_be_unique : BusinessRule<AddItemCommand>
    {
        public override async Task<ValidationError> ValidateAsync(AddItemCommand instance)
        {
            var target = await this.Domain.FindAsync<Item>(e => e.Name == instance.Name);
            if (target.Any())
            {
                return "The item name must be unique.";
            }
            return null;
        }
    }
}
