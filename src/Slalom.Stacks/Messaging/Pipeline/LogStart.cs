using System;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Logging;
using Slalom.Stacks.Messaging.Logging;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Messaging.Pipeline
{
    /// <summary>
    /// The log startup step of the Service execution pipeline.
    /// </summary>
    /// <seealso cref="Slalom.Stacks.Messaging.Pipeline.IMessageExecutionStep" />
    public class LogStart : IMessageExecutionStep
    {
        private readonly ILogger _logger;
        private readonly IRequestLog _requests;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogStart"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public LogStart(ILogger logger, IRequestLog requests)
        {
            Argument.NotNull(logger, nameof(logger));

            _logger = logger;
            _requests = requests;
        }

        /// <inheritdoc />
        public async Task Execute(ExecutionContext context)
        {
            await _requests.Append(context.Request).ConfigureAwait(false);

            var message = context.Request.Message;
            if (message.Body != null && context.Request.Path != null)
            {
                _logger.Verbose("Executing \"" + message.Name + "\" at path \"" + context.Request.Path + "\".");
            }
            else if (message.Body != null)
            {
                _logger.Verbose("Executing \"" + message.Name + ".");
            }
            else
            {
                _logger.Verbose("Executing message at path \"" + context.Request.Path + "\".");
            }
        }
    }
}