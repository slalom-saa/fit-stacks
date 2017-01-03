using System;
using System.Collections.Generic;
using Slalom.Stacks.Messaging.Validation;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Test.Examples.Actors.Items.Add.Rules
{
    public class add_item_input_is_valid : InputRule<AddItemCommand>
    {
        protected override IEnumerable<ValidationError> Validate(AddItemCommand instance)
        {
            if (String.IsNullOrWhiteSpace(instance.Text))
            {
                yield return "Text must be set to add an item.";
            }
        }
    }
}
