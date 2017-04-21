using System;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Services.Messaging;

namespace Slalom.Stacks.Services.Pipeline
{
    /// <summary>
    /// A defined step of the usecase execution pipeline.
    /// </summary>
    public interface IMessageExecutionStep
    {
        /// <summary>
        /// Executes the step of the message execution pipeline.
        /// </summary>
        /// <param name="context">The execution context.</param>
        /// <returns>A task for asynchronous programming.</returns>
        Task Execute(ExecutionContext context);
    }
}