using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using Slalom.Stacks.Messaging;

namespace Slalom.Stacks.Actors.Imp.Messages
{
    public class ExecuteUseCaseMessage : CommandMessage
    {
        public ExecuteUseCaseMessage(ICommand command, IActorRef caller)
            : base(command, caller)
        {
        }
    }
}
