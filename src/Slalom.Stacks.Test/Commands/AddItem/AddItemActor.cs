using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Slalom.Stacks.Communication;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Messaging.Actors;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Test.Domain;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Test.Commands.AddItem
{
    public class AddItemActor : UseCaseActor<AddItemCommand, AddItemEvent>
    {
        private readonly IDomainFacade _domain;

        public AddItemActor(IDomainFacade domain)
        {
            _domain = domain;
        }
        public override async Task<AddItemEvent> ExecuteAsync(AddItemCommand command, ExecutionContext context)
        {
            if (command.Text == "error")
            {
                throw new Exception("Throwing an example error.");
            }

            var target = Item.Create(command.Text);

            await _domain.AddAsync(target);

            return new AddItemEvent(target);
        }
    }
}