using System;
using System.Collections.Generic;
using System.Linq;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Communication.Validation
{
    /// <summary>
    /// Contains common <see cref="object"/> rules.
    /// </summary>
    public static class ObjectRules
    {
        /// <summary>
        /// Creates a not empty rule.
        /// </summary>
        /// <param name="rule">The current property rule.</param>
        /// <param name="message">The message to add if the instance is empty.</param>
        /// <returns>Returns the created rule.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="rule"/> argument is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="message"/> argument is null.</exception>
        public static PropertyRule<IEnumerable<T>> NotEmpty<T>(this PropertyRule<IEnumerable<T>> rule, ValidationError message)
        {
            if (rule == null)
            {
                throw new ArgumentNullException(nameof(rule));
            }
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }
            return rule.Then = new PropertyRule<IEnumerable<T>>(message, (e, b) => e.Any());
        }

        /// <summary>
        /// Creates a not empty rule.
        /// </summary>
        /// <param name="rule">The current property rule.</param>
        /// <param name="message">The message to add if the instance is empty.</param>
        /// <returns>Returns the created rule.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="rule"/> argument is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="message"/> argument is null.</exception>
        public static PropertyRule<List<T>> NotEmpty<T>(this PropertyRule<List<T>> rule, ValidationError message)
        {
            if (rule == null)
            {
                throw new ArgumentNullException(nameof(rule));
            }
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }
            return rule.Then = new PropertyRule<List<T>>(message, (e, b) => e.Any());
        }

        /// <summary>
        /// Creates a not null rule.
        /// </summary>
        /// <param name="rule">The current property rule.</param>
        /// <param name="message">The message to add if the instance is empty.</param>
        /// <returns>Returns the created rule.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="rule"/> argument is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="message"/> argument is null.</exception>
        public static PropertyRule<T> NotNull<T>(this PropertyRule<T> rule, ValidationError message)
        {
            if (rule == null)
            {
                throw new ArgumentNullException(nameof(rule));
            }
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }
            return rule.Then = new PropertyRule<T>(message, (e, b) => e != null);
        }
    }
}