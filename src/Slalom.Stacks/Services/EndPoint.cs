using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Newtonsoft.Json;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Services.Pipeline;
using Slalom.Stacks.Reflection;
using Slalom.Stacks.Search;
using Slalom.Stacks.Services.Logging;
using Slalom.Stacks.Services.Messaging;

namespace Slalom.Stacks.Services
{
    /// <summary>
    /// An endpoint is a single unit of solution logic that can be accessed in-process or out-of-process.  This endpoint type does not receive message data and does
    /// not return a value.
    /// </summary>
    public abstract class EndPoint : IEndPoint<object>
    {
        ExecutionContext IEndPoint.Context { get; set; }

        private ExecutionContext Context => ((IEndPoint)this).Context;

        /// <summary>
        /// Gets the current request.
        /// </summary>
        /// <value>The current request.</value>
        public Request Request => this.Context.Request;

        /// <summary>
        /// Gets the configured <see cref="IDomainFacade"/>.
        /// </summary>
        /// <value>The configured <see cref="IDomainFacade"/>.</value>
        public IDomainFacade Domain => this.Components.Resolve<IDomainFacade>();

        /// <summary>
        /// Gets the configured <see cref="ISearchFacade"/>.
        /// </summary>
        /// <value>The configured <see cref="ISearchFacade"/>.</value>
        public ISearchFacade Search => this.Components.Resolve<ISearchFacade>();

        /// <summary>
        /// Sends the specified command to the configured point-to-point endpoint.
        /// </summary>
        /// <param name="message">The message to send.</param>
        /// <returns>A task for asynchronous programming.</returns>
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

        /// <summary>
        /// Sends the specified command to the configured point-to-point endpoint.
        /// </summary>
        /// <param name="message">The message to send.</param>
        /// <returns>A task for asynchronous programming.</returns>
        public async Task<MessageResult> Send<T>(object message)
        {
            var result = await this.Send(message);

            if (result.Response is String)
            {
                result.Response = JsonConvert.DeserializeObject<T>((String)result.Response);
            }
            else
            {
                result.Response = JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(result.Response));
            }

            return result;
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

        /// <summary>
        /// Receives the call to the endpoint.
        /// </summary>
        public virtual void Receive()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Receives the call to the endpoint.
        /// </summary>
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

        /// <summary>
        /// Responds to the request with the specified message.
        /// </summary>
        /// <param name="instance">The message instance to respond with.</param>
        protected void Respond(object instance)
        {
            this.Context.Response = instance;
        }
    }

    /// <summary>
    /// An endpoint is a single unit of solution logic that can be accessed in-process or out-of-process.  This endpoint type takes in a message
    /// of the specified type and does not return a value.
    /// </summary>
    public abstract class EndPoint<TMessage> : IEndPoint<TMessage>
    {
        /// <summary>
        /// Sends the specified command to the configured point-to-point endpoint.
        /// </summary>
        /// <param name="message">The message to send.</param>
        /// <returns>A task for asynchronous programming.</returns>
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

        /// <summary>
        /// Sends the specified command to the configured point-to-point endpoint.
        /// </summary>
        /// <param name="message">The message to send.</param>
        /// <returns>A task for asynchronous programming.</returns>
        public async Task<MessageResult> Send<T>(object message)
        {
            var result = await this.Send(message);

            if (result.Response is String)
            {
                result.Response = JsonConvert.DeserializeObject<T>((String)message);
            }
            else
            {
                result.Response = JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(message));
            }

            return result;
        }

        ExecutionContext IEndPoint.Context { get; set; }

        private ExecutionContext Context => ((IEndPoint)this).Context;

        /// <summary>
        /// Gets the current request.
        /// </summary>
        /// <value>The current request.</value>
        public Request Request => this.Context.Request;

        /// <summary>
        /// Gets the configured <see cref="IDomainFacade"/>.
        /// </summary>
        /// <value>The configured <see cref="IDomainFacade"/>.</value>
        public IDomainFacade Domain => this.Components.Resolve<IDomainFacade>();

        /// <summary>
        /// Gets the configured <see cref="ISearchFacade"/>.
        /// </summary>
        /// <value>The configured <see cref="ISearchFacade"/>.</value>
        public ISearchFacade Search => this.Components.Resolve<ISearchFacade>();

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

        /// <summary>
        /// Receives the call to the endpoint.
        /// </summary>
        public virtual void Receive(TMessage instance)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Receives the call to the endpoint.
        /// </summary>
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

    /// <summary>
    /// An endpoint is a single unit of solution logic that can be accessed in-process or out-of-process.  This endpoint type takes in a message
    /// of the specified type and returns a value of the specified type.
    /// </summary>
    public abstract class EndPoint<TMessage, TResponse> : IEndPoint<TMessage, TResponse>
    {
        /// <summary>
        /// Sends the specified command to the configured point-to-point endpoint.
        /// </summary>
        /// <param name="message">The message to send.</param>
        /// <returns>A task for asynchronous programming.</returns>
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

        /// <summary>
        /// Sends the specified command to the configured point-to-point endpoint.
        /// </summary>
        /// <param name="message">The message to send.</param>
        /// <returns>A task for asynchronous programming.</returns>
        public async Task<MessageResult> Send<T>(object message)
        {
            var result = await this.Send(message);

            if (result.Response is String)
            {
                result.Response = JsonConvert.DeserializeObject<T>((String)message);
            }
            else
            {
                result.Response = JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(message));
            }

            return result;
        }

        ExecutionContext IEndPoint.Context { get; set; }

        private ExecutionContext Context => ((IEndPoint)this).Context;

        /// <summary>
        /// Gets the current request.
        /// </summary>
        /// <value>The current request.</value>
        public Request Request => this.Context.Request;

        /// <summary>
        /// Gets the configured <see cref="IDomainFacade"/>.
        /// </summary>
        /// <value>The configured <see cref="IDomainFacade"/>.</value>
        public IDomainFacade Domain => this.Components.Resolve<IDomainFacade>();

        /// <summary>
        /// Gets the configured <see cref="ISearchFacade"/>.
        /// </summary>
        /// <value>The configured <see cref="ISearchFacade"/>.</value>
        public ISearchFacade Search => this.Components.Resolve<ISearchFacade>();


        /// <summary>
        /// Gets the configured <see cref="IComponentContext"/> instance.
        /// </summary>
        /// <value>The configured <see cref="IComponentContext"/> instance.</value>
        internal IComponentContext Components { get; set; }

        /// <summary>
        /// Receives the call to the endpoint.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>Returns the response to the request.</returns>
        public virtual TResponse Receive(TMessage instance)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Receives the call to the endpoint.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>Returns the response to the request.</returns>
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