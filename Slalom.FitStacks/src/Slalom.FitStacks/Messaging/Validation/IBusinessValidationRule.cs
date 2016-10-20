using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Slalom.FitStacks.Runtime;
using Slalom.FitStacks.Validation;

namespace Slalom.FitStacks.Messaging.Validation
{
    /// <summary>
    /// Defines a contract for a business validation rule.
    /// </summary>
    /// <typeparam name="TCommand">The type of command to validate.</typeparam>
    public interface IBusinessValidationRule<in TCommand> : IAsyncValidationRule<TCommand, ExecutionContext> where TCommand : ICommand
    {
    }
}