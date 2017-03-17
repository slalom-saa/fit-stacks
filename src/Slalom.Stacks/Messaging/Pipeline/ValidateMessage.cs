using System;
using Autofac;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Messaging.Validation;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Messaging.Pipeline
{
    /// <summary>
    /// The validate message step of the usecase execution pipeline.
    /// </summary>
    /// <seealso cref="Slalom.Stacks.Messaging.Pipeline.IMessageExecutionStep" />
    public class ValidateMessage : IMessageExecutionStep
    {
        private readonly IComponentContext _components;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateMessage"/> class.
        /// </summary>
        /// <param name="components">The context.</param>
        public ValidateMessage(IComponentContext components)
        {
            Argument.NotNull(components, nameof(components));

            _components = components;
        }

        /// <inheritdoc />
        public async Task Execute(IMessage message, ExecutionContext context)
        {
            var validator = (IMessageValidator)_components.Resolve(typeof(MessageValidator<>).MakeGenericType(context.EndPoint.RequestType));
            var results = await validator.Validate(message.Body, context);
            context.AddValidationErrors(results);
        }
    }
}