using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Autofac;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Messaging.Events;
using Slalom.Stacks.Messaging.Pipeline;
using Slalom.Stacks.Search;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Services
{
    public class SystemEndPoint<T> : EndPoint<T> where T : class
    {
        internal override Task Prepare()
        {
            return Task.FromResult(0);
        }

        internal override Task Complete()
        {
            return Task.FromResult(0);
        }
    }
    public class SystemEndPoint<T, R> : EndPoint<T, R> where T : class where R : class
    {
        internal override Task Prepare()
        {
            return Task.FromResult(0);
        }

        internal override Task Complete()
        {
            return Task.FromResult(0);
        }
    }

    public interface IService
    {
        Request Request { get; }

        ExecutionContext Context { get; set; }
    }

    public abstract class Service : IService
    {
        public Request Request => ((IService)this).Context.Request;

        ExecutionContext IService.Context { get; set; }

        private ExecutionContext Context => ((IService)this).Context;

        /// <summary>
        /// Adds the raised event that will fire on completion.
        /// </summary>
        /// <param name="instance">The instance to raise.</param>
        public void AddRaisedEvent(Event instance)
        {
            Argument.NotNull(instance, nameof(instance));

            this.Context.AddRaisedEvent(instance);
        }

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="message">The message.</param>
        /// <returns>A task for asynchronous programming.</returns>
        protected Task<MessageResult> Send(string path, object message)
        {
            var stream = this.Components.Resolve<IMessageGateway>();

            return stream.Send(path, message, this.Context);
        }

        /// <summary>
        /// Gets the configured <see cref="IDomainFacade"/> instance.
        /// </summary>
        /// <value>The configured <see cref="IDomainFacade"/> instance.</value>    
        public IDomainFacade Domain => this.Components.Resolve<IDomainFacade>();

        /// <summary>
        /// Gets the configured <see cref="ISearchFacade"/> instance.
        /// </summary>
        /// <value>The configured <see cref="ISearchFacade"/> instance.</value>
        public ISearchFacade Search => this.Components.Resolve<ISearchFacade>();

        /// <summary>
        /// Gets the configured <see cref="IComponentContext"/> instance.
        /// </summary>
        /// <value>The configured <see cref="IComponentContext"/> instance.</value>
        internal IComponentContext Components { get; set; }

        /// <summary>
        /// Completes the specified message.
        /// </summary>
        /// <returns>A task for asynchronous programming.</returns>
        internal virtual async Task Complete()
        {
            var steps = new List<IMessageExecutionStep>
            {
                this.Components.Resolve<HandleException>(),
                this.Components.Resolve<Complete>(),
                this.Components.Resolve<PublishEvents>(),
                this.Components.Resolve<LogCompletion>()
            };
            foreach (var step in steps)
            {
                await step.Execute(this.Request.Message, this.Context);
            }
        }

        /// <summary>
        /// Prepares the usecase for execution.
        /// </summary>
        /// <returns>A task for asynchronous programming.</returns>
        internal virtual async Task Prepare()
        {
            var steps = new List<IMessageExecutionStep>
            {
                this.Components.Resolve<LogStart>(),
                this.Components.Resolve<ValidateMessage>()
            };
            foreach (var step in steps)
            {
                await step.Execute(this.Request.Message, this.Context);
            }
        }
    }

    public abstract class EndPoint<TCommand, TResult> : EndPoint<TCommand>, IEndPoint<TCommand> where TCommand : class where TResult : class
    {
        /// <summary>
        /// Executes the use case given the specified message.
        /// </summary>
        /// <param name="command">The message containing the input.</param>
        /// <returns>The message result.</returns>
        public new virtual TResult Receive(TCommand command)
        {
            throw new NotImplementedException($"The execution methods for the {this.GetType().Name} use case actor have not been implemented.");
        }

        /// <summary>
        /// Executes the use case given the specified message.
        /// </summary>
        /// <param name="command">The message containing the input.</param>
        /// <returns>A task for asynchronous programming.</returns>
        public new virtual Task<TResult> ReceiveAsync(TCommand command)
        {
            return Task.FromResult(this.Receive(command));
        }

        /// <inheritdoc />
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
                        var result = await this.ReceiveAsync(instance);

                        context.Response = result;

                        if (result is Event)
                        {
                            this.AddRaisedEvent(result as Event);
                        }
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

    public abstract class EndPoint<TCommand> : Service, IEndPoint<TCommand> where TCommand : class
    {

        /// <summary>
        /// Executes the use case given the specified message.
        /// </summary>
        /// <param name="command">The message containing the input.</param>
        /// <returns>The message result.</returns>
        public virtual void Receive(TCommand command)
        {
            throw new NotImplementedException($"The execution methods for the {this.GetType().Name} use case actor have not been implemented.");
        }

        /// <summary>
        /// Executes the use case given the specified message.
        /// </summary>
        /// <param name="command">The message containing the input.</param>
        /// <returns>A task for asynchronous programming.</returns>
        public virtual Task ReceiveAsync(TCommand command)
        {
            this.Receive(command);
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
                        await this.ReceiveAsync(instance);
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
