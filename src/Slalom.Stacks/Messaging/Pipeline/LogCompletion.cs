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
        private IEnumerable<IRequestStore> _requests;
        private IEnumerable<IEventStore> _events;
        private ILogger _logger;

        public LogCompletion(IComponentContext context)
        {
            _requests = context.Resolve<IEnumerable<IRequestStore>>();
            _events = context.Resolve<IEnumerable<IEventStore>>();
            _logger = context.Resolve<ILogger>();
        }

        public Task Execute(IMessage instance, MessageExecutionContext context)
        {
            var tasks = _requests.Select(e => e.AppendAsync(new RequestEntry(context.Request))).ToList();
            foreach (var item in context.RaisedEvents)
            {
                tasks.AddRange(_events.Select(e => e.AppendAsync(new EventEntry(item, context))));
            }

            var name = context.Request.RequestName;
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
