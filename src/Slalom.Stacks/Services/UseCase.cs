using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Autofac;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Messaging.Pipeline;
using Slalom.Stacks.Search;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Services
{
    /// <summary>
    /// Defines a use case actor that performs a defined function.
    /// </summary>
    /// <typeparam name="TCommand">The type of message.</typeparam>
    /// <typeparam name="TResult">The type of result.</typeparam>
    public abstract class UseCase<TCommand, TResult> : UseCase<TCommand>, IHandle<TCommand> where TCommand : class
    {
        /// <summary>
        /// Executes the use case given the specified message.
        /// </summary>
        /// <param name="command">The message containing the input.</param>
        /// <returns>The message result.</returns>
        public new virtual TResult Execute(TCommand command)
        {
            throw new NotImplementedException($"The execution methods for the {this.GetType().Name} use case actor have not been implemented.");
        }

        /// <summary>
        /// Executes the use case given the specified message.
        /// </summary>
        /// <param name="command">The message containing the input.</param>
        /// <returns>A task for asynchronous programming.</returns>
        public new virtual Task<TResult> ExecuteAsync(TCommand command)
        {
            return Task.FromResult(this.Execute(command));
        }

        /// <inheritdoc />
        async Task IHandle<TCommand>.Handle(TCommand instance)
        {
            await this.Prepare();

            if (!this.Context.ValidationErrors.Any())
            {
                try
                {
                    if (!this.Context.CancellationToken.IsCancellationRequested)
                    {
                        var result = await this.ExecuteAsync(instance);

                        this.Context.Response = result;
                    }
                }
                catch (Exception exception)
                {
                    this.Context.SetException(exception);
                }
            }

            await this.Complete();
        }
    }

    /// <summary>
    /// Defines a use case actor that performs a defined function.
    /// </summary>
    /// <typeparam name="TCommand">The type of message.</typeparam>
    public abstract class UseCase<TCommand> : Service, IHandle<TCommand> where TCommand : class
    {
        /// <summary>
        /// Gets the configured <see cref="IDomainFacade"/> instance.
        /// </summary>
        /// <value>The configured <see cref="IDomainFacade"/> instance.</value>    
        public IDomainFacade Domain => this.Components.Resolve<IDomainFacade>();

        public Request Request => this.Context.Request;

        /// <summary>
        /// Gets the configured <see cref="ISearchFacade"/> instance.
        /// </summary>
        /// <value>The configured <see cref="ISearchFacade"/> instance.</value>
        public ISearchFacade Search => this.Components.Resolve<ISearchFacade>();

        /// <summary>
        /// Gets the user making the request.
        /// </summary>
        /// <value>The user making the request.</value>
        public IPrincipal User => this.Context.Request.User;

        /// <summary>
        /// Gets the configured <see cref="IComponentContext"/> instance.
        /// </summary>
        /// <value>The configured <see cref="IComponentContext"/> instance.</value>
        internal IComponentContext Components { get; set; }

        /// <summary>
        /// Adds the raised event that will fire on completion.
        /// </summary>
        /// <param name="instance">The instance to raise.</param>
        public void AddRaisedEvent(EventData instance)
        {
            Argument.NotNull(instance, nameof(instance));

            this.Context.AddRaisedEvent(instance);
        }

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

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>A task for asynchronous programming.</returns>
        protected Task<MessageResult> Send(object message)
        {
            var stream = this.Components.Resolve<IMessageGateway>();

            return stream.Send(message, this.Context);
        }

        /// <summary>
        /// Completes the specified message.
        /// </summary>
        /// <returns>A task for asynchronous programming.</returns>
        internal async Task Complete()
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
        internal async Task Prepare()
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

        async Task IHandle<TCommand>.Handle(TCommand instance)
        {
            await this.Prepare();

            if (!this.Context.ValidationErrors.Any())
            {
                try
                {
                    if (!this.Context.CancellationToken.IsCancellationRequested)
                    {
                        await this.ExecuteAsync(instance);
                    }
                }
                catch (Exception exception)
                {
                    this.Context.SetException(exception);
                }
            }

            await this.Complete();
        }
    }
}