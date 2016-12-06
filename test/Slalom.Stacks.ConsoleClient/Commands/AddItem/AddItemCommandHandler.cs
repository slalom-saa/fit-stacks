using System;
using System.Threading.Tasks;
using Slalom.FitStacks.ConsoleClient.Domain;
using Slalom.Stacks.Communication;

namespace Slalom.FitStacks.ConsoleClient.Commands.AddItem
{
    public class AddItemCommandHandler : CommandHandler<AddItemCommand, ItemAddedEvent>
    {
        public override async Task<ItemAddedEvent> Handle(AddItemCommand command)
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