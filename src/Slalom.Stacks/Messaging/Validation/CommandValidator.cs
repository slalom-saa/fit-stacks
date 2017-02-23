using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Messaging.Validation
{
    /// <summary>
    /// Validates a message using input, security and business rules.
    /// </summary>
    public class CommandValidator<TCommand> : ICommandValidator where TCommand : ICommand
    {
        private readonly IEnumerable<IValidate<TCommand>> _rules;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandValidator{TCommand}"/> class.
        /// </summary>
        /// <param name="rules">The rules for the message.</param>
        public CommandValidator(IEnumerable<IValidate<TCommand>> rules)
        {
            Argument.NotNull(rules, nameof(rules));

            _rules = rules;
        }

        /// <summary>
        /// Validates the specified message.
        /// </summary>
        /// <param name="command">The message to validate.</param>
        /// <returns>The <see cref="ValidationError">messages</see> returned from validation routines.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="command" /> argument is null.</exception>
        public Task<IEnumerable<ValidationError>> Validate(ICommand command)
        {
            Argument.NotNull(command, nameof(command));

            var instance = (TCommand)command;

            var input = this.CheckInputRules(instance).ToList();
            if (input.Any())
            {
                return Task.FromResult(input.WithType(ValidationErrorType.Input));
            }

            var security = this.CheckSecurityRules(instance).ToList();
            if (security.Any())
            {
                return Task.FromResult(security.WithType(ValidationErrorType.Security));
            }

            var business = this.CheckBusinessRules(instance).ToList();
            if (business.Any())
            {
                return Task.FromResult(business.WithType(ValidationErrorType.Business));
            }

            return Task.FromResult(Enumerable.Empty<ValidationError>());
        }

        /// <summary>
        /// Checks all discovered business rules for validation errors.
        /// </summary>
        /// <param name="command">The message to validate.</param>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="command" /> argument is null.</exception>
        /// <returns>A task for asynchronous programming.</returns>
        protected virtual IEnumerable<ValidationError> CheckBusinessRules(TCommand command)
        {
            foreach (var rule in _rules.OfType<IBusinessRule<TCommand>>())
            {
                var result = (rule.Validate(command)).ToList();
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
        /// <param name="command">The message to validate.</param>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="command" /> argument is null.</exception>
        /// <returns>A task for asynchronous programming.</returns>
        protected virtual IEnumerable<ValidationError> CheckInputRules(TCommand command)
        {
            var target = new List<ValidationError>();
            foreach (var rule in _rules.OfType<IInputRule<TCommand>>())
            {
                target.AddRange(rule.Validate(command));
            }
            foreach (var property in command.Type.GetProperties())
            {
                foreach (var attribute in property.GetCustomAttributes<ValidationAttribute>())
                {
                    if (!attribute.IsValid(property.GetValue(command)))
                    {
                        if (attribute.Code == null)
                        {
                            attribute.Code = $"{command.Type.Name}.{property.Name}.{attribute.GetType().Name.Replace("Attribute", "")}";
                        }
                        target.Add(attribute.ValidationError);
                    }
                }
            }

            return target.AsEnumerable();
        }

        /// <summary>
        /// Checks all discovered security rules for validation errors.
        /// </summary>
        /// <param name="command">The message to validate.</param>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="command" /> argument is null.</exception>
        /// <returns>A task for asynchronous programming.</returns>
        protected virtual IEnumerable<ValidationError> CheckSecurityRules(TCommand command)
        {
            foreach (var rule in _rules.OfType<ISecurityRule<TCommand>>())
            {
                var result = (rule.Validate(command)).ToList();
                if (result.Any())
                {
                    return result;
                }
            }
            return Enumerable.Empty<ValidationError>();
        }
    }
}