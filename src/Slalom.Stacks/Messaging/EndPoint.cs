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