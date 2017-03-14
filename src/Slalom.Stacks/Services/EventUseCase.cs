using System;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Messaging.Events;

namespace Slalom.Stacks.Services
{
    public abstract class EventUseCase<TCommand> : Service, IEndPoint<TCommand> where TCommand : Event
    {
        /// <summary>
        /// Executes the use case given the specified message.
        /// </summary>
        /// <param name="command">The message containing the input.</param>
        /// <returns>The message result.</returns>
        public virtual void Execute(TCommand command)
        {
            throw new NotImplementedException($"The execution methods for the {this.GetType().Name} use case actor have not been implemented.");
        }

        /// <summary>
        /// Executes the use case given the specified message.
        /// </summary>
        /// <param name="command">The message containing the input.</param>
        /// <returns>A task for asynchronous programming.</returns>
        public virtual Task ExecuteAsync(TCommand command)
        {
            this.Execute(command);

            return Task.FromResult(0);
        }

        async Task IEndPoint<TCommand>.Receive(TCommand instance)
        {
            await this.Prepare();

            var context = ((IService)this).Context;

            if (!context.ValidationErrors.Any())
            {
                try
                {
                    if (!context.CancellationToken.IsCancellationRequested)
                    {
                        await this.ExecuteAsync(instance);
                    }
                }
                catch (Exception exception)
                {
                    context.SetException(exception);
                }
            }

            await this.Complete();
        }
    }
}