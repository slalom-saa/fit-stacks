using Slalom.FitStacks.Runtime;
using Slalom.FitStacks.Validation;

namespace Slalom.FitStacks.Messaging.Validation
{
    /// <summary>
    /// Defines a contract for input validation of a command.
    /// </summary>
    /// <typeparam name="TValue">The type of the command to validate.</typeparam>
    public interface IInputValidationRule<in TValue> : IValidationRule<TValue, ExecutionContext> where TValue : ICommand
    {
    }
}