using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Actors.Imp.Messages
{
    public class UseCaseExecuted : ExecuteStepMessage
    {
        public UseCaseExecuted(ExecuteUseCase message)
            : base(message)
        {
        }
    }
}