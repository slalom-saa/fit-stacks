using System;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Communication.Validation
{
    /// <summary>
    /// Performs business validation on a command.
    /// </summary>
    /// <typeparam name="TCommand">The command type.</typeparam>
    /// <seealso cref="IBusinessValidationRule{TCommand}" />
    public abstract class BusinessValidationRule<TCommand> : IBusinessValidationRule<TCommand> where TCommand : ICommand
    {
        /// <summary>
        /// Gets the execution context.
        /// </summary>
        /// <value>The execution context.</value>
        public ExecutionContext Context { get; private set; }

        /// <summary>
        /// Validates the specified command instance.
        /// </summary>
        /// <param name="instance">The instance to validate.</param>
        /// <param name="context">The execution context.</param>
        /// <returns>A task for asynchronous programming.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="instance"/> argument is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="context"/> argument is null.</exception>
        public Task<ValidationError> Validate(TCommand instance, ExecutionContext context)
        {
            Argument.NotNull(() => instance);
            Argument.NotNull(() => context);

            this.Context = context;

            return this.Validate(instance);
        }

        /// <summary>
        /// Validates the specified command instance.
        /// </summary>
        /// <param name="instance">The instance to validate.</param>
        /// <returns>A task for asynchronous programming.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="instance"/> argument is null.</exception>
        protected abstract Task<ValidationError> Validate(TCommand instance);
    }
}
