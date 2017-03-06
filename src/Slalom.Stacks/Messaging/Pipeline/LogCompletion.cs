using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Slalom.Stacks.Logging;
using Slalom.Stacks.Messaging.Persistence;

namespace Slalom.Stacks.Messaging.Pipeline
{
    /// <summary>
    /// The log completion step of the use case execution pipeline.
    /// </summary>
    /// <seealso cref="Slalom.Stacks.Messaging.Pipeline.IMessageExecutionStep" />
    public class LogCompletion : IMessageExecutionStep
    {
        private IResponseStore _actions;
        private ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogCompletion"/> class.
        /// </summary>
        /// <param name="components">The component context.</param>
        public LogCompletion(IComponentContext components)
        {
            _actions = components.Resolve<IResponseStore>();
            _logger = components.Resolve<ILogger>();
        }

        /// <inheritdoc />
        public Task Execute(IMessage instance, ExecutionContext context)
        {
            var tasks = new List<Task> { _actions.Append(new ResponseEntry(context)) };

            var name = context.Request.Message.Name;
            if (!context.IsSuccessful)
            {
                if (context.Exception != null)
                {
                    _logger.Error(context.Exception, "An unhandled exception was raised while executing \"" + name + "\".", instance);
                }
                else if (context.ValidationErrors?.Any() ?? false)
                {
                    _logger.Error("Execution completed with validation errors while executing \"" + name + "\": " + String.Join("; ", context.ValidationErrors.Select(e => e.Type + ": " + e.Message)), instance);
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
