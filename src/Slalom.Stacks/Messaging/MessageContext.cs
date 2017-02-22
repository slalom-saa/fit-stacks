using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Messaging.Routing;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Messaging
{
    public class MessageContext
    {
        private readonly List<Event> _raisedEvents = new List<Event>();

        public MessageContext(string requestId, string requestName, string path, ExecutionContext execution, MessageContext parent = null)
        {
            this.Parent = parent;
            this.CorrelationId = execution.CorrelationId;
            this.RequestId = requestId;
            this.RequestName = requestName;
            this.Path = path;
        }

        public object Response { get; set; }

        public MessageContext Parent { get; }

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

        public string CorrelationId { get; }

        public string RequestId { get; }

        public string RequestName { get; }

        public string Path { get; }

        public void AddValidationErrors(IEnumerable<ValidationError> results)
        {
            _validationErrors.AddRange(results);
        }
    }
}
