using System;
using System.Collections.Generic;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Messaging.Validation
{
    /// <summary>
    /// Defines a contract for input validation of a message.
    /// </summary>
    /// <typeparam name="TValue">The type of the message to validate.</typeparam>
    public interface IInputRule<in TValue> : IValidationRule<TValue> where TValue : ICommand
    {
    }
}