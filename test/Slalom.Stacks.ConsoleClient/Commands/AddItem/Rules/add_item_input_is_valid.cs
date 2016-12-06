using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Communication.Validation;

namespace Slalom.FitStacks.ConsoleClient.Commands.AddItem.Rules
{
    public class add_item_input_is_valid : InputValidationRuleSet<AddItemCommand>
    {
        public add_item_input_is_valid()
        {
            this.Add(e => e.Text)
                .NotNullOrWhiteSpace("Text must be set to add an item.");
        }
    }
}
