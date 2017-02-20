using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Messaging.Exceptions
{
    public class ChainFailedException : Exception
    {
        public IMessage Command { get; }

        public MessageExecutionResult Child { get; }

        public ChainFailedException(IMessage command, MessageExecutionResult child) 
            : base($"Failed to complete command {command.Id} because of a failed dependent command {child.MessageId}.", child.RaisedException ?? new ValidationException(child.ValidationErrors.ToArray()))
        {
            this.Command = command;
            this.Child = child;
        }
    }
}
