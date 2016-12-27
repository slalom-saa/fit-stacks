using System;
using Akka.Actor;
using Slalom.Stacks.Messaging;

namespace Slalom.Stacks.Actors
{
    public class CommandMessage
    {
        private Type _commandType;

        public IActorRef Caller { get; }

        public ICommand Command { get; }

        public Type CommandType => _commandType ?? (_commandType = this.Command.GetType());

        public CommandMessage(ICommand command, IActorRef caller)
        {
            this.Command = command;
            this.Caller = caller;
        }
    }
}