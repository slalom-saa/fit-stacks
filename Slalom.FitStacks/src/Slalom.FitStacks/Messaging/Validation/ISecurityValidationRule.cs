using Slalom.FitStacks.Runtime;
using Slalom.FitStacks.Validation;

namespace Slalom.FitStacks.Messaging.Validation
{
    /// <summary>
    /// Defines a contract for security validation rules.
    /// </summary>
    /// <typeparam name="TCommand">The type of command to validate.</typeparam>
    public interface ISecurityValidationRule<in TCommand> : IAsyncValidationRule<TCommand, ExecutionContext> where TCommand : ICommand
    {
    }
}