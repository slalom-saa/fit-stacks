using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using Slalom.Stacks.Communication;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Actors.Imp.Messages
{
    public class UseCaseExecutionFailedMessage : CommandMessage
    {
        public IEnumerable<ValidationError> Errors { get; }

        public UseCaseExecutionFailedMessage(ICommand command, IActorRef caller, IEnumerable<ValidationError> errors)
            : base(command, caller)
        {
            this.Errors = errors;
        }
    }
}
