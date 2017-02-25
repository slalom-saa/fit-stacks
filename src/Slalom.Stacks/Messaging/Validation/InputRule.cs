using System;
using System.Collections.Generic;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Messaging.Validation
{
    /// <summary>
    /// Represents an input validation rule set that should be run as a single unit.
    /// </summary>
    /// <typeparam name="TCommand">The type of message.</typeparam>
    public abstract class InputRule<TCommand> : IInputRule<TCommand> where TCommand : ICommand
    {
        /// <summary>
        /// Validates the specified message instance.
        /// </summary>
        /// <param name="instance">The instance to validate.</param>
        /// <returns>A task for asynchronous programming.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="instance"/> argument is null.</exception>
        public abstract IEnumerable<ValidationError> Validate(TCommand instance);
    }
}