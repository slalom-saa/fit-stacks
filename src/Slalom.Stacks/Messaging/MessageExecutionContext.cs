using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Messaging
{
    public class MessageExecutionContext
    {
        private readonly List<Event> _raisedEvents = new List<Event>();

        public MessageExecutionContext(RequestContext request, LocalRegistryEntry entry, ExecutionContext execution, MessageExecutionContext parent = null)
        {
            this.Request = request;
            this.Entry = entry;
            this.Parent = parent;
            this.Execution = execution;
        }

        public object Response { get; set; }

        public RequestContext Request { get; }
        public LocalRegistryEntry Entry { get; }

        public MessageExecutionContext Parent { get; }

        public DateTimeOffset Started { get; } = DateTimeOffset.UtcNow;

        public DateTimeOffset? Completed { get; private set; }

        public IEnumerable<Event> RaisedEvents => _raisedEvents.AsEnumerable();

        public void Complete()
        {
            this.Completed = DateTimeOffset.UtcNow;
        }

        public void AddRaisedEvent(Event instance)
        {
            _raisedEvents.Add(instance);
        }

        private List<ValidationError> _validationErrors = new List<ValidationError>();

        public IEnumerable<ValidationError> ValidationErrors => _validationErrors.AsEnumerable();

        public bool IsSuccessful => !this.ValidationErrors.Any() && this.Exception == null;

        public Exception Exception { get; private set; }

        public void RaiseException(Exception exception)
        {
            this.Exception = exception;
        }

        public ExecutionContext Execution { get; }

        public void AddValidationErrors(IEnumerable<ValidationError> results)
        {
            _validationErrors.AddRange(results);
        }
    }
}
