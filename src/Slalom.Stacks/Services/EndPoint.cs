using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Services.Pipeline;
using Slalom.Stacks.Reflection;
using Slalom.Stacks.Search;
using Slalom.Stacks.Services.Messaging;

namespace Slalom.Stacks.Services
{
    public abstract class EndPoint : IEndPoint<object>
    {
        ExecutionContext IEndPoint.Context { get; set; }

        private ExecutionContext Context => ((IEndPoint)this).Context;

        public Request Request => this.Context.Request;

        public IDomainFacade Domain => this.Components.Resolve<IDomainFacade>();

        public Task<MessageResult> Send(object message)
        {
            return this.Components.Resolve<IMessageGateway>().Send(message, this.Context);
        }

        /// <summary>
        /// Adds an event to be raised when the execution is successful.
        /// </summary>
        /// <param name="instance">The event to add.</param>
        public void AddRaisedEvent(Event instance)
        {
            this.Context.AddRaisedEvent(instance);
        }

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

        async Task IEndPoint<object>.Receive(object instance)
        {
            await this.Components.Resolve<ValidateMessage>().Execute(this.Context);

            if (!this.Context.ValidationErrors.Any())
            {
                try
                {
                    if (!this.Context.CancellationToken.IsCancellationRequested)
                    {
                        await this.ReceiveAsync();
                    }
                }
                catch (Exception exception)
                {
                    this.Context.SetException(exception);
                }
            }
        }

        protected void Respond(object instance)
        {
            this.Context.Response = instance;
        }
    }

    public abstract class EndPoint<TMessage> : IEndPoint<TMessage>
    {
        public Task<MessageResult> Send(object message)
        {
            return this.Components.Resolve<IMessageGateway>().Send(message, this.Context);
        }

        ExecutionContext IEndPoint.Context { get; set; }

        private ExecutionContext Context => ((IEndPoint)this).Context;

        public Request Request => this.Context.Request;

        public IDomainFacade Domain => this.Components.Resolve<IDomainFacade>();

        /// <summary>
        /// Adds an event to be raised when the execution is successful.
        /// </summary>
        /// <param name="instance">The event to add.</param>
        public void AddRaisedEvent(Event instance)
        {
            this.Context.AddRaisedEvent(instance);
        }

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
            await this.Components.Resolve<ValidateMessage>().Execute(this.Context);

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
        public Task<MessageResult> Send(object message)
        {
            var attribute = message.GetType().GetAllAttributes<RequestAttribute>().FirstOrDefault();
            if (attribute != null)
            {
                return this.Components.Resolve<IMessageGateway>().Send(attribute.Path, message, this.Context);
            }
            else
            {
                return this.Components.Resolve<IMessageGateway>().Send(message, this.Context);
            }
        }

        ExecutionContext IEndPoint.Context { get; set; }

        private ExecutionContext Context => ((IEndPoint)this).Context;

        public Request Request => this.Context.Request;

        public IDomainFacade Domain => this.Components.Resolve<IDomainFacade>();


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
            await this.Components.Resolve<ValidateMessage>().Execute(this.Context);

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
}