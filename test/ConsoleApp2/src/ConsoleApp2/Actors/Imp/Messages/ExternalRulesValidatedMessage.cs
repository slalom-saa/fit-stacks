using System.Collections.Generic;
using Akka.Actor;
using Slalom.Stacks.Communication;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Actors
{
    public class ExternalRulesValidatedMessage : CommandMessage
    {
        public IEnumerable<ValidationError> ValidationResults { get; }

        public ExternalRulesValidatedMessage(ICommand command, IActorRef caller, IEnumerable<ValidationError> validationResults) : base(command, caller)
        {
            this.ValidationResults = validationResults;
        }
    }
}