using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Actors;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks
{
    public class AddProcedure : UseCaseActor<AddProcedureCommand, ProcedureAddedEvent>
    {
        public override ProcedureAddedEvent Execute(AddProcedureCommand command)
        {
            return new ProcedureAddedEvent(command.Name);
        }

        public override IEnumerable<ValidationError> Validate(AddProcedureCommand command)
        {
            yield return "Adsf";
        }
    }
}
