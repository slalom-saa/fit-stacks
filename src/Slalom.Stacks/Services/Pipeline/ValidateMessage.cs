﻿/* 
 * Copyright (c) Stacks Contributors
 * 
 * This file is subject to the terms and conditions defined in
 * the LICENSE file, which is part of this source code package.
 */

using System.Threading.Tasks;
using Autofac;
using Slalom.Stacks.Services.Messaging;
using Slalom.Stacks.Services.Validation;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Services.Pipeline
{
    /// <summary>
    /// The validate message step of the usecase execution pipeline.
    /// </summary>
    /// <seealso cref="Slalom.Stacks.Services.Pipeline.IMessageExecutionStep" />
    internal class ValidateMessage : IMessageExecutionStep
    {
        private readonly IComponentContext _components;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateMessage" /> class.
        /// </summary>
        /// <param name="components">The context.</param>
        public ValidateMessage(IComponentContext components)
        {
            Argument.NotNull(components, nameof(components));

            _components = components;
        }

        /// <inheritdoc />
        public async Task Execute(ExecutionContext context)
        {
            var message = context.Request.Message;

            if (message.Body != null)
            {
                var validator = (IMessageValidator) _components.Resolve(typeof(MessageValidator<>).MakeGenericType(context.EndPoint.RequestType));
                var results = await validator.Validate(message.Body, context);
                context.AddValidationErrors(results);
            }
        }
    }
}