using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Slalom.Stacks.Communication;
using Slalom.Stacks.Messaging.Actors;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Test.Domain;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Test.Commands.AddItem
{
    public class AddItemActor : UseCaseActor<AddItemCommand, AddItemEvent>
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