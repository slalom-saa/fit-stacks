using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Logging;

namespace Slalom.Stacks.Messaging.Pipeline
{
    public class LogStart : IMessageExecutionStep
    {
        private readonly ILogger _logger;

        public LogStart(ILogger logger)
        {
            _logger = logger;
        }

        public Task Execute(IMessage message, MessageContext context)
        {
            _logger.Verbose("Executing \"" + context.RequestName + "\" at path \"" + context.Path + "\".");

            return Task.FromResult(0);
        }
    }
}
