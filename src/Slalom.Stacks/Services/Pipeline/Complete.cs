/* 
 * Copyright (c) Stacks Contributors
 * 
 * This file is subject to the terms and conditions defined in
 * the LICENSE file, which is part of this source code package.
 */

using System.Threading.Tasks;
using Slalom.Stacks.Services.Messaging;

namespace Slalom.Stacks.Services.Pipeline
{
    /// <summary>
    /// The completion step of the usecase execution pipeline.
    /// </summary>
    /// <seealso cref="Slalom.Stacks.Services.Pipeline.IMessageExecutionStep" />
    public class Complete : IMessageExecutionStep
    {
        /// <inheritdoc />
        public Task Execute(ExecutionContext context)
        {
            context.Complete();

            return Task.FromResult(0);
        }
    }
}