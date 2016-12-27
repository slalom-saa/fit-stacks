using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Actors;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Validation;
using Akka.DI.Core;

namespace Slalom.Stacks
{
    public class Procedure : Entity, IAggregateRoot
    {
        public string Name { get; }

        public Procedure(string name)
        {
            this.Name = name;
        }
    }

    public class AddProcedure : UseCaseActor<AddProcedureCommand, ProcedureAddedEvent>
    {
        private readonly IDomainFacade _domain;

        public AddProcedure(IDomainFacade domain)
        {
            _domain = domain;
        }

        public override async Task<ProcedureAddedEvent> ExecuteAsync(AddProcedureCommand command)
        {
            var target = new Procedure(command.Name);

            await _domain.AddAsync(target);

            return new ProcedureAddedEvent(target.Name);
        }

        public override IEnumerable<ValidationError> Validate(AddProcedureCommand command)
        {
            //var target = _domain.FindAsync<Procedure>(e => e.Name == command.Name).Result;
            //if (target != null)
            //{
            //    yield return "The thing already exists.";
            //}
            yield break;
        }
    }
}
