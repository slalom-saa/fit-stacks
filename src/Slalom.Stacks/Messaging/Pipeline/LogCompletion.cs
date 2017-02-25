using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Slalom.Stacks.Logging;
using Slalom.Stacks.Messaging.Logging;

namespace Slalom.Stacks.Messaging.Pipeline
{
    /// <summary>
    /// The log completion step of the use case execution pipeline.
    /// </summary>
    /// <seealso cref="Slalom.Stacks.Messaging.Pipeline.IMessageExecutionStep" />
    public class LogCompletion : IMessageExecutionStep
    {
        private IEnumerable<IResponseStore> _actions;
        private ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogCompletion"/> class.
        /// </summary>
        /// <param name="components">The component context.</param>
        public LogCompletion(IComponentContext components)
        {
            _actions = components.Resolve<IEnumerable<IResponseStore>>();
            _logger = components.Resolve<ILogger>();
        }

        /// <inheritdoc />
        public Task Execute(IMessage instance, MessageExecutionContext context)
        {
            var tasks = _actions.Select(e => e.Append(new ResponseEntry(context))).ToList();

            var name = context.RequestContext.Message.GetType().FullName;
            if (!context.IsSuccessful)
            {
                if (context.Exception != null)
                {
                    _logger.Error(context.Exception, "An unhandled exception was raised while executing \"" + name + "\".", instance);
                }
                else if (context.ValidationErrors?.Any() ?? false)
                {
                    _logger.Verbose("Execution completed with validation errors while executing \"" + name + "\": " + String.Join("; ", context.ValidationErrors.Select(e => e.ErrorType + ": " + e.Message)), instance);
                }
                else
                {
                    _logger.Error("Execution completed unsuccessfully while executing \"" + name + "\".", instance);
                }
            }
            else
            {
                _logger.Verbose("Successfully executed \"" + name + "\".", instance);
            }

            return Task.WhenAll(tasks);
        }
    }
}
