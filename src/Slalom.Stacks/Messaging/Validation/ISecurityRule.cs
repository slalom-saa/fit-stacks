using Slalom.Stacks.Runtime;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Messaging.Validation
{
    /// <summary>
    /// Validates an object instance based on security rules.
    /// </summary>
    /// <typeparam name="TValue">The type to validate.</typeparam>
    public interface ISecurityRule<in TValue> : IValidate<TValue>
    {
    }
}