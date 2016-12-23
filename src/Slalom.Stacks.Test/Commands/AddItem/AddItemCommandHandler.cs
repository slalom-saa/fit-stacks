using System;
using System.Threading.Tasks;
using Slalom.Stacks.Communication;
using Slalom.Stacks.Test.Domain;

namespace Slalom.Stacks.Test.Commands.AddItem
{
    public class AddItemCommandHandler : CommandHandler<AddItemCommand, ItemAddedEvent>
    {
        public override async Task<ItemAddedEvent> HandleAsync(AddItemCommand command)
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