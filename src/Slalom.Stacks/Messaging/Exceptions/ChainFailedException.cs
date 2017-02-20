using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slalom.Stacks.Messaging.Exceptions
{
    public class ChainFailedException : Exception
    {
        public Command Command { get; }

        public MessageExecutionResult Child { get; }

        public ChainFailedException(Command command, MessageExecutionResult child)
        {
            this.Command = command;
            this.Child = child;
        }
    }
}
