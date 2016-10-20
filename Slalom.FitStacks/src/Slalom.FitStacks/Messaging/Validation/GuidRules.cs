using System;
using Slalom.FitStacks.Validation;

namespace Slalom.FitStacks.Messaging.Validation
{
    /// <summary>
    /// Contains common <see cref="Guid"/> rules.
    /// </summary>
    public static class GuidRules
    {
        /// <summary>
        /// Creates a not empty rule.
        /// </summary>
        /// <param name="rule">The current property rule.</param>
        /// <param name="message">The message to add if the instance is empty.</param>
        /// <returns>Returns the created rule.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="rule"/> argument is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="message"/> argument is null.</exception>
        public static PropertyRule<Guid> NotEmpty(this PropertyRule<Guid> rule, ValidationError message)
        {
            if (rule == null)
            {
                throw new ArgumentNullException(nameof(rule));
            }
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }
            return rule.Then = new PropertyRule<Guid>(message, (e, b) => e != Guid.Empty);
        }
    }
}