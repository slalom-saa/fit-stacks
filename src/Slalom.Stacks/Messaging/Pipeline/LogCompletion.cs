using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Slalom.Stacks.Logging;
using Slalom.Stacks.Messaging.Logging;

namespace Slalom.Stacks.Messaging.Pipeline
{
    public class LogCompletion : IMessageExecutionStep
    {
        private IEnumerable<IActionStore> _actions;
        private ILogger _logger;

        public LogCompletion(IComponentContext context)
        {
            _actions = context.Resolve<IEnumerable<IActionStore>>();
            _logger = context.Resolve<ILogger>();
        }

        public Task Execute(IMessage instance, MessageExecutionContext context)
        {
            var tasks = _actions.Select(e => e.Append(new ResponseEntry(context))).ToList();

            var name = context.Request.MessageName;
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
