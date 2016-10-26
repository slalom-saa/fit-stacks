using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Messaging.Validation
{
    /// <summary>
    /// Defines a contract for a business validation rule.
    /// </summary>
    /// <typeparam name="TCommand">The type of command to validate.</typeparam>
    public interface IBusinessValidationRule<in TCommand> : IAsyncValidationRule<TCommand, ExecutionContext> where TCommand : ICommand
    {
    }
}