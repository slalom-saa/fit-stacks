using System;
using System.Collections.Generic;
using Autofac;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Messaging.Pipeline;
using Slalom.Stacks.Search;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Messaging
{
    /// <summary>
    /// Defines a use case actor that performs a defined function.
    /// </summary>
    /// <typeparam name="TCommand">The type of message.</typeparam>
    /// <typeparam name="TResult">The type of result.</typeparam>
    public abstract class UseCase<TCommand, TResult> : UseCase, IHandle<TCommand> where TCommand : ICommand
    {
        /// <summary>
        /// Executes the use case given the specified message.
        /// </summary>
        /// <param name="command">The message containing the input.</param>
        /// <returns>The message result.</returns>
        public virtual TResult Execute(TCommand command)
        {
            throw new NotImplementedException($"The execution methods for the {this.GetType().Name} use case actor have not been implemented.");
        }

        /// <summary>
        /// Executes the use case given the specified message.
        /// </summary>
        /// <param name="command">The message containing the input.</param>
        /// <returns>A task for asynchronous programming.</returns>
        public virtual Task<TResult> ExecuteAsync(TCommand command)
        {
            return Task.FromResult(this.Execute(command));
        }

        /// <inheritdoc />
        public async Task Handle(TCommand instance)
        {
            await this.Prepare(instance);

            if (!this.Context.ValidationErrors.Any())
            {
                try
                {
                    var result = await this.ExecuteAsync(instance);

                    this.Context.Response = result;
                }
                catch (Exception exception)
                {
                    this.Context.SetException(exception);
                }
            }

            await this.Complete(instance);
        }

        /// <summary>
        /// Validates the specified message.
        /// </summary>
        /// <param name="command">The message to validate.</param>
        /// <returns>Any validation errors.</returns>
        public virtual IEnumerable<ValidationError> Validate(TCommand command)
        {
            yield break;
        }

        /// <summary>
        /// Validates the specified message.
        /// </summary>
        /// <param name="command">The message to validate.</param>
        /// <returns>Any validation errors.</returns>
        public virtual Task<IEnumerable<ValidationError>> ValidateAsync(TCommand command)
        {
            return Task.FromResult(this.Validate(command));
        }
    }

    /// <summary>
    /// The base usecase class.
    /// </summary>
    /// <seealso cref="Slalom.Stacks.Messaging.UseCase" />
    /// <seealso cref="Slalom.Stacks.Messaging.IHandle{TCommand}" />
    public abstract class UseCase : IUseMessageContext
    {
        /// <summary>
        /// Gets the configured <see cref="IComponentContext"/> instance.
        /// </summary>
        /// <value>The configured <see cref="IComponentContext"/> instance.</value>
        protected IComponentContext Components { get; set; }

        /// <summary>
        /// Gets the current <see cref="MessageExecutionContext"/> instance.
        /// </summary>
        /// <value>The current <see cref="MessageExecutionContext"/> instance.</value>
        protected MessageExecutionContext Context { get; private set; }

        /// <summary>
        /// Gets the configured <see cref="IDomainFacade"/> instance.
        /// </summary>
        /// <value>The configured <see cref="IDomainFacade"/> instance.</value>    
        protected IDomainFacade Domain => this.Components.Resolve<IDomainFacade>();

        /// <summary>
        /// Gets the configured <see cref="ISearchFacade"/> instance.
        /// </summary>
        /// <value>The configured <see cref="ISearchFacade"/> instance.</value>
        protected ISearchFacade Search => this.Components.Resolve<ISearchFacade>();

        /// <inheritdoc />
        public void UseContext(MessageExecutionContext context)
        {
            this.Context = context;
        }

        /// <summary>
        /// Completes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>A task for asynchronous programming.</returns>
        protected async Task Complete(IMessage message)
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
                await step.Execute(message, this.Context);
            }
        }

        /// <summary>
        /// Prepares the usecase for execution.
        /// </summary>
        /// <param name="message">The current message.</param>
        /// <returns>A task for asynchronous programming.</returns>
        protected async Task Prepare(IMessage message)
        {
            var steps = new List<IMessageExecutionStep>
            {
                this.Components.Resolve<LogStart>(),
                this.Components.Resolve<ValidateMessage>()
            };
            foreach (var step in steps)
            {
                await step.Execute(message, this.Context);
            }
        }

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>A task for asynchronous programming.</returns>
        protected Task<MessageResult> Send(ICommand message)
        {
            var stream = this.Components.Resolve<IMessageGateway>();

            return stream.Send(message, this.Context);
        }
    }

    /// <summary>
    /// Defines a use case actor that performs a defined function.
    /// </summary>
    /// <typeparam name="TCommand">The type of message.</typeparam>
    public abstract class UseCase<TCommand> : UseCase, IHandle<TCommand> where TCommand : IMessage
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

        /// <inheritdoc />
        public async Task Handle(TCommand instance)
        {
            await this.Prepare(instance);

            if (!this.Context.ValidationErrors.Any())
            {
                try
                {
                    await this.ExecuteAsync(instance);
                }
                catch (Exception exception)
                {
                    this.Context.SetException(exception);
                }
            }

            await this.Complete(instance);
        }

        /// <summary>
        /// Validates the specified message.
        /// </summary>
        /// <param name="command">The message to validate.</param>
        /// <returns>Any validation errors.</returns>
        public virtual IEnumerable<ValidationError> Validate(TCommand command)
        {
            yield break;
        }

        /// <summary>
        /// Validates the specified message.
        /// </summary>
        /// <param name="command">The message to validate.</param>
        /// <returns>Any validation errors.</returns>
        public virtual Task<IEnumerable<ValidationError>> ValidateAsync(TCommand command)
        {
            return Task.FromResult(this.Validate(command));
        }
    }
}