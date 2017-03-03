using System;
using System.Collections.Generic;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Messaging.Validation
{
    /// <summary>
    /// Validates and object instance using input rules.
    /// </summary>
    /// <typeparam name="TValue">The type to validate.</typeparam>
    public interface IInputRule<in TValue> : IValidate<TValue>
    {
    }
}