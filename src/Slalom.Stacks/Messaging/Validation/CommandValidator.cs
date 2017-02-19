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
    /// Validates a command using input, security and business rules.
    /// </summary>
    public class CommandValidator<TCommand> : ICommandValidator where TCommand : IMessage
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
        /// <returns>The <see cref="ValidationError">messages</see> returned from validation routines.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="instance" /> argument is null.</exception>
        public Task<IEnumerable<ValidationError>> Validate(MessageEnvelope instance)
        {
            Argument.NotNull(instance, nameof(instance));

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
        /// <param name="instance">The instance.</param>
        /// <returns>A task for asynchronous programming.</returns>
        protected virtual IEnumerable<ValidationError> CheckBusinessRules(MessageEnvelope instance)
        {
            foreach (var rule in _rules.OfType<IBusinessValidationRule<TCommand>>())
            {
                var result = (rule.Validate(instance)).ToList();
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
        /// <param name="instance">The instance.</param>
        /// <returns>A task for asynchronous programming.</returns>
        protected virtual IEnumerable<ValidationError> CheckInputRules(MessageEnvelope instance)
        {
            var target = new List<ValidationError>();
            foreach (var rule in _rules.OfType<IInputValidationRule<TCommand>>())
            {
                target.AddRange(rule.Validate(instance));
            }
            foreach (var property in instance.Message.Type.GetProperties())
            {
                foreach (var attribute in property.GetCustomAttributes<ValidationAttribute>())
                {
                    if (!attribute.IsValid(property.GetValue(instance.Message)))
                    {
                        if (attribute.Code == null)
                        {
                            attribute.Code = $"{instance.Message.Type.Name}.{property.Name}.{attribute.GetType().Name.Replace("Attribute", "")}";
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
        /// <param name="instance">The instance.</param>
        /// <returns>A task for asynchronous programming.</returns>
        protected virtual IEnumerable<ValidationError> CheckSecurityRules(MessageEnvelope instance)
        {
            foreach (var rule in _rules.OfType<ISecurityValidationRule<TCommand>>())
            {
                var result = (rule.Validate(instance)).ToList();
                if (result.Any())
                {
                    return result;
                }
            }
            return Enumerable.Empty<ValidationError>();
        }
    }
}