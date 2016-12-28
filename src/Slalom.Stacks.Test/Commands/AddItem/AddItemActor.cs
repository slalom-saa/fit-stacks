using System;
using System.Threading.Tasks;
using Slalom.Stacks.Communication;
using Slalom.Stacks.Messaging.Actors;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Test.Domain;

namespace Slalom.Stacks.Test.Commands.AddItem
{
    public class AddItemActor : UseCaseActor<AddItemCommand, ItemAddedEvent>
    {
        public override async Task<ItemAddedEvent> ExecuteAsync(AddItemCommand command, ExecutionContext context)
        {
            if (command.Text == "error")
            {
                throw new Exception("Throwing an example error.");
            }

            var target = Item.Create(command.Text);

            await this.Domain.AddAsync(target);

            return new ItemAddedEvent(target);
        }
    }
}