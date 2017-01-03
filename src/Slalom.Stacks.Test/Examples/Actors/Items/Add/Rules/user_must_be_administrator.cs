using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Slalom.Stacks.Messaging.Validation;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Test.Examples.Actors.Items.Add.Rules
{
    public class user_must_be_administrator : SecurityValidationRule<AddItemCommand>
    {
        protected override IEnumerable<ValidationError> Validate(AddItemCommand instance)
        {
            if (!this.Context.User.Claims.Any(e => e.Type == ClaimTypes.Role && e.Value == "Administrator"))
            {
                yield return "You must be an administrator to add an item.";
            }
        }
    }
}