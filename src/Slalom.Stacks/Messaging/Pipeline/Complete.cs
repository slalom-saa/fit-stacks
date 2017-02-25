using System;
using System.Linq;
using System.Threading.Tasks;

namespace Slalom.Stacks.Messaging.Pipeline
{
    /// <summary>
    /// The completion step of the usecase execution pipeline.
    /// </summary>
    /// <seealso cref="Slalom.Stacks.Messaging.Pipeline.IMessageExecutionStep" />
    public class Complete : IMessageExecutionStep
    {
        /// <inheritdoc />
        public Task Execute(IMessage message, MessageExecutionContext context)
        {
            context.Complete();

            return Task.FromResult(0);
        }
    }
}