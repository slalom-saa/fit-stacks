using System.Collections.Generic;
using Slalom.Stacks.Messaging.Validation;
using Slalom.Stacks.Validation;

namespace WebApplication1.Actors.Products.Update.Rules
{
    public class command_is_valid : InputRule<UpdateProductCommand>
    {
        protected override IEnumerable<ValidationError> Validate(UpdateProductCommand instance)
        {
            if (string.IsNullOrWhiteSpace(instance.Id))
            {
                yield return "The product ID cannot be null or whitespace.";
            }
        }
    }
}