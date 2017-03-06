using System;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Logging;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Messaging.Pipeline
{
    /// <summary>
    /// The log startup step of the usecase execution pipeline.
    /// </summary>
    /// <seealso cref="Slalom.Stacks.Messaging.Pipeline.IMessageExecutionStep" />
    public class LogStart : IMessageExecutionStep
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogStart"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public LogStart(ILogger logger)
        {
            Argument.NotNull(logger, nameof(logger));

            _logger = logger;
        }

        /// <inheritdoc />
        public Task Execute(IMessage message, ExecutionContext context)
        {
            _logger.Verbose("Executing \"" + message.MessageType.FullName + "\" at path \"" + context.Request.Path + "\".");

            return Task.FromResult(0);
        }
    }
}