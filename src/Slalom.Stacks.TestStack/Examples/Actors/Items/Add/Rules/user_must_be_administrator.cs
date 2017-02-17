using System;
using System.Collections.Generic;
using Slalom.Stacks.Messaging.Validation;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.TestStack.Examples.Actors.Items.Add.Rules
{
    public class user_must_be_administrator : SecurityRule<AddItemCommand>
    {
        protected override IEnumerable<ValidationError> Validate(AddItemCommand instance)
        {
            if (!this.UserInRole("Administrator"))
            {
                yield return "You must be an administrator to add an item.";
            }
        }
    }
}