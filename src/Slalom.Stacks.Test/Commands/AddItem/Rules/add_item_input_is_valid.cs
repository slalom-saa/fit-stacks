using System;
using System.Collections.Generic;
using System.Linq;
using Slalom.Stacks.Communication.Validation;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Test.Commands.AddItem.Rules
{
    public class add_item_input_is_valid : InputValidationRule<AddItemCommand>
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
