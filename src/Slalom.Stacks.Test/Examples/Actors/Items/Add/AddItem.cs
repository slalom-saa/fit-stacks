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
    [Path("items/add")]
    public class AddItem : UseCaseActor<AddItemCommand, AddItemEvent>
    {
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