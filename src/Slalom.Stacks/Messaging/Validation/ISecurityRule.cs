using Slalom.Stacks.Runtime;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Messaging.Validation
{
    /// <summary>
    /// Defines a contract for security validation rules.
    /// </summary>
    /// <typeparam name="TCommand">The type of message to validate.</typeparam>
    public interface ISecurityRule<in TCommand> : IValidate<TCommand> where TCommand : ICommand
    {
    }
}