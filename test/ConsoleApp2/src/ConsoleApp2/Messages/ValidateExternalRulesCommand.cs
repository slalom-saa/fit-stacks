using System;
using Akka.Actor;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Runtime;

namespace Slalom.Stacks.Actors
{
    public class ExecuteStepMessage
    {
        public ExecuteUseCase ExecutionMessage { get; set; }

        public ExecuteStepMessage(ExecuteUseCase message)
        {
            this.ExecutionMessage = message;
        }

        public Type CommandType => this.ExecutionMessage.CommandType;

        public ICommand Command => this.ExecutionMessage.Command;

        public ExecutionContext Context => this.ExecutionMessage.Context;

        public CommandResult Result => this.ExecutionMessage.Result;
    }

    public class ValidateRules : ExecuteStepMessage
    {
        public ValidateRules(ExecuteUseCase message)
            : base(message)
        {
        }
    }
}