using System;
using System.Collections.Generic;
using Slalom.Stacks.Messaging.Validation;
using Slalom.Stacks.Validation;

namespace WebApplication1.Actors.Products.Expire.Rules
{
    public class command_is_valid : InputRule<ExpireProductCommand>
    {
        protected override IEnumerable<ValidationError> Validate(ExpireProductCommand instance)
        {
            if (String.IsNullOrWhiteSpace(instance.Id))
            {
                yield return "The product Id cannot be null or whitespace.";
            }
        }
    }
}