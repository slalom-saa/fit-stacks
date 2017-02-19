using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Messaging.Exceptions
{
    public interface IExecutionExceptionHandler
    {
        void HandleException(MessageEnvelope instance, MessageExecutionResult result, Exception exception);
    }

    public class ExecutionExceptionHandler : IExecutionExceptionHandler
    {
        public virtual void HandleException(MessageEnvelope instance, MessageExecutionResult result, Exception exception)
        {
            var validationException = exception as ValidationException;
            if (validationException != null)
            {
                result.AddValidationErrors(validationException.ValidationMessages);
            }
            else if (exception is AggregateException)
            {
                var innerException = exception.InnerException as ValidationException;
                if (innerException != null)
                {
                    result.AddValidationErrors(innerException.ValidationMessages);
                }
                else if (exception.InnerException is TargetInvocationException)
                {
                    result.RaisedException = ((TargetInvocationException)exception.InnerException).InnerException;
                }
                else
                {
                    result.RaisedException = exception.InnerException;
                }
            }
            else if (exception is TargetInvocationException)
            {
                result.RaisedException = exception.InnerException;
            }
            else
            {
                result.RaisedException = exception;
            }
        }
    }
}
