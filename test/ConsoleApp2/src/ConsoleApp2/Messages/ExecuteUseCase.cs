using System;
using Akka.Actor;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Runtime;

namespace Slalom.Stacks.Actors
{
    public class ExecuteUseCase
    {
        private Type _commandType;

        public ICommand Command { get; }

        public ExecutionContext Context { get; }

        public CommandExecuted Result { get; }

        public Type CommandType => _commandType ?? (_commandType = this.Command.GetType());

        public ExecuteUseCase(ICommand command, ExecutionContext context)
        {
            this.Command = command;
            this.Context = context;
            this.Result = new CommandExecuted(context);
        }
    }
}