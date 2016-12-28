using System.Collections.Generic;
using Akka.Actor;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Actors
{
    public class RulesValidated : ExecuteStepMessage
    {
        public RulesValidated(ExecuteUseCase message)
            : base(message)
        {
        }
    }
}