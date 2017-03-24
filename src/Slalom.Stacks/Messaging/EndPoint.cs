using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Messaging.Pipeline;
using Slalom.Stacks.Search;

namespace Slalom.Stacks.Messaging
{
    public abstract class EndPoint : IEndPoint<object>
    {
        ExecutionContext IEndPoint.Context { get; set; }

        private ExecutionContext Context => ((IEndPoint)this).Context;

        public Request Request => this.Context.Request;

        /// <summary>
        /// Gets the configured <see cref="IComponentContext"/> instance.
        /// </summary>
        /// <value>The configured <see cref="IComponentContext"/> instance.</value>
        internal IComponentContext Components { get; set; }

        public virtual void Receive()
        {
            throw new NotImplementedException();
        }

        public virtual Task ReceiveAsync()
        {
            this.Receive();
            return Task.FromResult(0);
        }

        Task IEndPoint<object>.Receive(object instance)
        {
            return this.ReceiveAsync();
        }

        protected void Respond(object instance)
        {
            this.Context.Response = instance;
        }
    }

    public abstract class EndPoint<TMessage> : IEndPoint<TMessage>
    {
        ExecutionContext IEndPoint.Context { get; set; }

        private ExecutionContext Context => ((IEndPoint)this).Context;

        public Request Request => this.Context.Request;

        /// <summary>
        /// Gets the configured <see cref="IComponentContext"/> instance.
        /// </summary>
        /// <value>The configured <see cref="IComponentContext"/> instance.</value>
        internal IComponentContext Components { get; set; }

        public virtual void Receive(TMessage instance)
        {
            throw new NotImplementedException();
        }

        public virtual Task ReceiveAsync(TMessage instance)
        {
            this.Receive(instance);
            return Task.FromResult(0);
        }

        async Task IEndPoint<TMessage>.Receive(TMessage instance)
        {
            if (!this.Context.ValidationErrors.Any())
            {
                try
                {
                    if (!this.Context.CancellationToken.IsCancellationRequested)
                    {
                        await this.ReceiveAsync(instance);
                    }
                }
                catch (Exception exception)
                {
                    this.Context.SetException(exception);
                }
            }
        }
    }

    public abstract class EndPoint<TMessage, TResponse> : IEndPoint<TMessage, TResponse>
    {
        ExecutionContext IEndPoint.Context { get; set; }

        private ExecutionContext Context => ((IEndPoint)this).Context;

        public Request Request => this.Context.Request;

        /// <summary>
        /// Gets the configured <see cref="IComponentContext"/> instance.
        /// </summary>
        /// <value>The configured <see cref="IComponentContext"/> instance.</value>
        internal IComponentContext Components { get; set; }

        public virtual TResponse Receive(TMessage instance)
        {
            throw new NotImplementedException();
        }

        public virtual Task<TResponse> ReceiveAsync(TMessage instance)
        {
            return Task.FromResult(this.Receive(instance));

        }

        /// <summary>
        /// Adds an event to be raised when the execution is successful.
        /// </summary>
        /// <param name="instance">The event to add.</param>
        public void AddRaisedEvent(Event instance)
        {
            this.Context.AddRaisedEvent(instance);
        }

        async Task<TResponse> IEndPoint<TMessage, TResponse>.Receive(TMessage instance)
        {
            TResponse result = default(TResponse);
            if (!this.Context.ValidationErrors.Any())
            {
                try
                {
                    if (!this.Context.CancellationToken.IsCancellationRequested)
                    {
                        result = await this.ReceiveAsync(instance);

                        this.Context.Response = result;

                        if (result is Event)
                        {
                            this.AddRaisedEvent(result as Event);
                        }
                    }
                }
                catch (Exception exception)
                {
                    this.Context.SetException(exception);
                }
            }
            return result;
        }
    }

    public abstract class UseCase<TCommand, TResult> : UseCase<TCommand>, IEndPoint<TCommand> where TCommand : class where TResult : class
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
        async Task IEndPoint<TCommand>.Receive(TCommand instance)
        {
            if (!this.Context.ValidationErrors.Any())
            {
                try
                {
                    if (!this.Context.CancellationToken.IsCancellationRequested)
                    {
                        var result = await this.ExecuteAsync(instance);

                        this.Context.Response = result;

                        if (result is Event)
                        {
                            this.AddRaisedEvent(result as Event);
                        }
                    }
                }
                catch (Exception exception)
                {
                    this.Context.SetException(exception);
                }
            }
        }



        private ExecutionContext Context => ((IEndPoint)this).Context;
    }

    public abstract class UseCase<TCommand> : EndPoint<TCommand>, IEndPoint<TCommand> where TCommand : class
    {
        /// <summary>
        /// Adds an event to be raised when the execution is successful.
        /// </summary>
        /// <param name="instance">The event to add.</param>
        public void AddRaisedEvent(Event instance)
        {
            this.Context.AddRaisedEvent(instance);
        }

        private ExecutionContext Context => ((IEndPoint)this).Context;

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
        /// <returns>Returns .</returns>
        public Task<MessageResult> Send(object message)
        {
            return this.Components.Resolve<IMessageGateway>().Send(message);
        }

        async Task IEndPoint<TCommand>.Receive(TCommand instance)
        {
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
        }
    }
}