using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Messaging.Pipeline.Steps
{
    public class HandleException : IMessageExecutionStep
    {
        public Task Execute(IMessage message, MessageContext context)
        {
            var exception = context.Exception;
            var validationException = exception as ValidationException;
            if (validationException != null)
            {
                context.AddValidationErrors(validationException.ValidationErrors);
            }
            else if (exception is AggregateException)
            {
                var innerException = exception.InnerException as ValidationException;
                if (innerException != null)
                {
                    context.AddValidationErrors(innerException.ValidationErrors);
                }
                else if (exception.InnerException is TargetInvocationException)
                {
                    context.RaiseException(((TargetInvocationException)exception.InnerException).InnerException);
                }
                else
                {
                    context.RaiseException(exception.InnerException);
                }
            }
            else if (exception is TargetInvocationException)
            {
                context.RaiseException(exception.InnerException);
            }
            else
            {
                context.RaiseException(exception);
            }
            return Task.FromResult(0);
        }
    }
}
