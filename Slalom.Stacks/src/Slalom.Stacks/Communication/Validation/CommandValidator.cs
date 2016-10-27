using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Slalom.Stacks.Configuration;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Communication.Validation
{
    /// <summary>
    /// Validates commands using input, security and business rules.
    /// </summary>
    public class CommandValidator : ICommandValidator
    {
        private readonly IComponentContext _componentContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandValidator"/> class.
        /// </summary>
        /// <param name="componentContext">The current <see cref="IComponentContext"/> instance.</param>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="componentContext"/> argument is null.</exception>
        public CommandValidator(IComponentContext componentContext)
        {
            Argument.NotNull(() => componentContext);

            _componentContext = componentContext;
        }

        /// <summary>
        /// Validates the specified command.
        /// </summary>
        /// <typeparam name="TResponse">The type of response expected for the command.</typeparam>
        /// <param name="command">The command to validate.</param>
        /// <param name="context">The current execution context.</param>
        /// <returns>The <see cref="ValidationError">messages</see> returned from validation routines.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="command"/> argument is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="context"/> argument is null.</exception>
        public async Task<IEnumerable<ValidationError>> Validate<TResponse>(Command<TResponse> command, ExecutionContext context)
        {
            Argument.NotNull(() => command);
            Argument.NotNull(() => context);

            var input = (this.CheckInputRules(command, context)).ToList();
            if (input.Any())
            {
                return input.WithType(ValidationErrorType.Input);
            }

            var security = (await this.CheckSecurityRules(command, context)).ToList();
            if (security.Any())
            {
                return security.WithType(ValidationErrorType.Security);
            }

            var business = (await this.CheckBusinessRules(command, context)).ToList();
            if (business.Any())
            {
                return business.WithType(ValidationErrorType.Business);
            }

            return Enumerable.Empty<ValidationError>();
        }

        /// <summary>
        /// Checks all discovered business rules for validation errors.
        /// </summary>
        /// <typeparam name="TResponse">The command response type.</typeparam>
        /// <param name="command">The command to validate.</param>
        /// <param name="context">The execution context.</param>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="command" /> argument is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="context" /> argument is null.</exception>
        /// <returns>A task for asynchronous programming.</returns>
        protected virtual async Task<IEnumerable<ValidationError>> CheckBusinessRules<TResponse>(Command<TResponse> command, ExecutionContext context)
        {
            Argument.NotNull(() => command);
            Argument.NotNull(() => context);

            var type = typeof(IBusinessValidationRule<>).MakeGenericType(command.GetType());
            var sets = _componentContext.ResolveAll(type);

            var method = typeof(IAsyncValidationRule<,>).MakeGenericType(command.GetType(), context.GetType()).GetMethod("Validate");

            foreach (var set in sets)
            {
                var result = await (Task<ValidationError>)method.Invoke(set, new object[] { command, context });
                if (result != null)
                {
                    return new[] { result };
                }
            }
            return Enumerable.Empty<ValidationError>();
        }

        /// <summary>
        /// Checks all discovered input rules for validation errors.
        /// </summary>
        /// <typeparam name="TResponse">The command response type.</typeparam>
        /// <param name="command">The command to validate.</param>
        /// <param name="context">The execution context.</param>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="command" /> argument is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="context" /> argument is null.</exception>
        /// <returns>A task for asynchronous programming.</returns>
        protected virtual IEnumerable<ValidationError> CheckInputRules<TResponse>(Command<TResponse> command, ExecutionContext context)
        {
            Argument.NotNull(() => command);
            Argument.NotNull(() => context);

            var type = typeof(IInputValidationRule<>).MakeGenericType(command.GetType());
            var sets = _componentContext.ResolveAll(type);

            var method = typeof(IValidationRule<,>).MakeGenericType(command.GetType(), context.GetType()).GetMethod("Validate");

            return sets.SelectMany(e => (IEnumerable<ValidationError>)method.Invoke(e, new object[] { command, context }))
                       .Select(e => e.WithType(ValidationErrorType.Input));
        }

        /// <summary>
        /// Checks all discovered security rules for validation errors.
        /// </summary>
        /// <typeparam name="TResponse">The command response type.</typeparam>
        /// <param name="command">The command to validate.</param>
        /// <param name="context">The execution context.</param>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="command" /> argument is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="context" /> argument is null.</exception>
        /// <returns>A task for asynchronous programming.</returns>
        protected virtual async Task<IEnumerable<ValidationError>> CheckSecurityRules<TResponse>(Command<TResponse> command, ExecutionContext context)
        {
            Argument.NotNull(() => command);
            Argument.NotNull(() => context);

            var type = typeof(ISecurityValidationRule<>).MakeGenericType(command.GetType());
            var sets = _componentContext.ResolveAll(type);

            var method = typeof(IAsyncValidationRule<,>).MakeGenericType(command.GetType(), context.GetType()).GetMethod("Validate");

            foreach (var set in sets)
            {
                var result = await (Task<ValidationError>)method.Invoke(set, new object[] { command, context });
                if (result != null)
                {
                    return new[] { result };
                }
            }
            return Enumerable.Empty<ValidationError>();
        }
    }
}