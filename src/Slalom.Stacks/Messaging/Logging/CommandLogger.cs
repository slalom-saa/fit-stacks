using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Logging;

namespace Slalom.Stacks.Messaging.Logging
{
    public class CommandLogger : ICommandLogger
    {
        private readonly IEnumerable<IRequestStore> _requests;
        private readonly IEnumerable<IEventStore> _events;
        private readonly ILogger _logger;

        public CommandLogger(IEnumerable<IRequestStore> requests, IEnumerable<IEventStore> events, ILogger logger)
        {
            _requests = requests;
            _events = events;
            _logger = logger;
        }

        public virtual Task LogCompletion(MessageEnvelope instance, MessageExecutionResult result, IHandle handler)
        {
            var tasks = _requests.Select(e => e.AppendAsync(new RequestEntry(instance, result))).ToList();
            foreach (var item in instance.Context.RaisedEvents)
            {
                tasks.AddRange(_events.Select(e => e.AppendAsync(new EventEntry(item, instance.Context))));
            }

            var message = instance.Message;

            var name = handler.GetType().Name;
            if (!result.IsSuccessful)
            {
                if (result.RaisedException != null)
                {
                    _logger.Error(result.RaisedException, "An unhandled exception was raised while executing \"" + name + "\".", message);
                }
                else if (result.ValidationErrors?.Any() ?? false)
                {
                    _logger.Verbose("Execution completed with validation errors while executing \"" + name + "\": " + String.Join("; ", result.ValidationErrors.Select(e => e.ErrorType + ": " + e.Message)), message);
                }
                else
                {
                    _logger.Error("Execution completed unsuccessfully while executing \"" + name + "\".", message);
                }
            }
            else
            {
                _logger.Verbose("Successfully executed \"" + name + "\".", message);
            }

            return Task.WhenAll(tasks);
        }

        public Task LogStart(MessageEnvelope instance, IHandle handler)
        {
            _logger.Verbose("Executing \"" + handler.GetType().Name + "\" at path \"" + instance.Context.Path + "\".");

            return Task.FromResult(0);
        }
    }
}
