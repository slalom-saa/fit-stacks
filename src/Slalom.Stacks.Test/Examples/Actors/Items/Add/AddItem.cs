using System;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Test.Examples.Domain;

namespace Slalom.Stacks.Test.Examples.Actors.Items.Add
{
    [Path("items/add")]
    public class AddItem : UseCaseActor<AddItemCommand, AddItemEvent>
    {
        public override async Task<AddItemEvent> ExecuteAsync(AddItemCommand command)
        {
            if (command.Name == "error")
            {
                throw new Exception("Throwing an example error.");
            }

            var target = Item.Create(command.Name);

            await this.Domain.AddAsync(target);

            await Task.Delay(5);

            return new AddItemEvent(target);
        }
    }
}