using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Messaging.Validation
{
    /// <summary>
    /// Validates a command using input, security and business rules.
    /// </summary>
    public class CommandValidator<TCommand> : ICommandValidator where TCommand : ICommand
    {
        private readonly IEnumerable<IValidationRule<TCommand, ExecutionContext>> _rules;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandValidator{TCommand}"/> class.
        /// </summary>
        /// <param name="rules">The rules for the command.</param>
        public CommandValidator(IEnumerable<IValidationRule<TCommand, ExecutionContext>> rules)
        {
            Argument.NotNull(rules, nameof(rules));

            _rules = rules;
        }

        /// <summary>
        /// Validates the specified command.
        /// </summary>
        /// <param name="command">The command to validate.</param>
        /// <param name="context">The current execution context.</param>
        /// <returns>The <see cref="ValidationError">messages</see> returned from validation routines.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="command"/> argument is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="context"/> argument is null.</exception>
        public async Task<IEnumerable<ValidationError>> Validate(ICommand command, ExecutionContext context)
        {
            Argument.NotNull(command, nameof(command));
            Argument.NotNull(context, nameof(context));

            var instance = (TCommand)command;

            var input = this.CheckInputRules(instance, context).ToList();
            if (input.Any())
            {
                return input.WithType(ValidationErrorType.Input);
            }

            var security = this.CheckSecurityRules(instance, context).ToList();
            if (security.Any())
            {
                return security.WithType(ValidationErrorType.Security);
            }

            var business = this.CheckBusinessRules(instance, context).ToList();
            if (business.Any())
            {
                return business.WithType(ValidationErrorType.Business);
            }

            return Enumerable.Empty<ValidationError>();
        }

        /// <summary>
        /// Checks all discovered business rules for validation errors.
        /// </summary>
        /// <param name="command">The command to validate.</param>
        /// <param name="context">The execution context.</param>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="command" /> argument is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="context" /> argument is null.</exception>
        /// <returns>A task for asynchronous programming.</returns>
        protected virtual IEnumerable<ValidationError> CheckBusinessRules(TCommand command, ExecutionContext context)
        {
            foreach (var rule in _rules.OfType<IBusinessValidationRule<TCommand>>())
            {
                var result = (rule.Validate(command, context)).ToList();
                if (result.Any())
                {
                    return result;
                }
            }
            return Enumerable.Empty<ValidationError>();
        }

        /// <summary>
        /// Checks all discovered input rules for validation errors.
        /// </summary>
        /// <param name="command">The command to validate.</param>
        /// <param name="context">The execution context.</param>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="command" /> argument is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="context" /> argument is null.</exception>
        /// <returns>A task for asynchronous programming.</returns>
        protected virtual IEnumerable<ValidationError> CheckInputRules(TCommand command, ExecutionContext context)
        {
            var target = new List<ValidationError>();
            foreach (var rule in _rules.OfType<IInputValidationRule<TCommand>>())
            {
                target.AddRange(rule.Validate(command, context));
            }
            return target.AsEnumerable();
        }

        /// <summary>
        /// Checks all discovered security rules for validation errors.
        /// </summary>
        /// <param name="command">The command to validate.</param>
        /// <param name="context">The execution context.</param>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="command" /> argument is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="context" /> argument is null.</exception>
        /// <returns>A task for asynchronous programming.</returns>
        protected virtual IEnumerable<ValidationError> CheckSecurityRules(TCommand command, ExecutionContext context)
        {
            foreach (var rule in _rules.OfType<ISecurityValidationRule<TCommand>>())
            {
                var result = (rule.Validate(command, context)).ToList();
                if (result.Any())
                {
                    return result;
                }
            }
            return Enumerable.Empty<ValidationError>();
        }
    }
}