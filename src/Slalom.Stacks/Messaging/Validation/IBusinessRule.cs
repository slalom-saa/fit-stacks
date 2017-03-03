using System;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Messaging.Validation
{
    /// <summary>
    /// Validates an object instance using a business rule.
    /// </summary>
    /// <typeparam name="TValue">The type of message to validate.</typeparam>
    public interface IBusinessRule<in TValue> : IValidate<TValue>
    {
    }
}