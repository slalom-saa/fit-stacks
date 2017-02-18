using System;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Messaging.Validation
{
    /// <summary>
    /// Defines a contract for a business validation rule.
    /// </summary>
    /// <typeparam name="TCommand">The type of command to validate.</typeparam>
    public interface IBusinessValidationRule<in TCommand> : IValidationRule<TCommand, ExecutionContext> where TCommand : IMessage
    {
    }
}