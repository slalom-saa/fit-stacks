using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Messaging.Exceptions
{
    public class ChainFailedException : Exception
    {
        public IMessage Message { get; }

        public MessageResult Child { get; }

        public ChainFailedException(IMessage message, MessageResult child)
            : base($"Failed to complete message {message.Id} because of a failed dependent message {child.RequestId}.", child.RaisedException ?? new ValidationException(child.ValidationErrors.ToArray()))
        {
            this.Message = message;
            this.Child = child;
        }
    }
}
