using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Test.Examples.Domain;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Test.Examples.Actors.Items.Add
{
    public class AddItem : UseCaseActor<AddItemCommand, AddItemEvent>
    {
        //public override IEnumerable<ValidationError> Validate(AddItemCommand command, ExecutionContext context)
        //{
        //    var current = this.Domain.FindAsync<Item>(e => e.Text == command.Text).Result.Any();
        //    if (current)
        //    {
        //        //yield return "Up";
        //    }
        //    yield break;
        //}

        public override async Task<AddItemEvent> ExecuteAsync(AddItemCommand command, ExecutionContext context)
        {
            if (command.Text == "error")
            {
                throw new Exception("Throwing an example error.");
            }

            var target = Item.Create(command.Text);

            await this.Domain.AddAsync(target);

            return new AddItemEvent(target);
        }
    }
}