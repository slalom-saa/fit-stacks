﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Slalom.Stacks.Reflection;
using Slalom.Stacks.Services.Inventory;
using Slalom.Stacks.Services.Logging;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Services.Messaging
{
    /// <summary>
    /// A default <see cref="IMessageGateway" /> implementation.
    /// </summary>
    public class MessageGateway : IMessageGateway
    {
        private readonly IComponentContext _components;
        private readonly Lazy<ILocalMessageDispatcher> _dispatcher;
        private readonly Lazy<IRequestContext> _requestContext;
        private readonly Lazy<IRequestLog> _requests;
        private readonly Lazy<ServiceInventory> _services;
        private Lazy<IEnumerable<IRemoteMessageDispatcher>> _dispatchers;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageGateway" /> class.
        /// </summary>
        /// <param name="components">The components.</param>
        public MessageGateway(IComponentContext components)
        {
            Argument.NotNull(components, nameof(components));

            _components = components;
            _services = new Lazy<ServiceInventory>(components.Resolve<ServiceInventory>);
            _requestContext = new Lazy<IRequestContext>(components.Resolve<IRequestContext>);
            _requests = new Lazy<IRequestLog>(components.Resolve<IRequestLog>);
            _dispatcher = new Lazy<ILocalMessageDispatcher>(components.Resolve<ILocalMessageDispatcher>);
            _dispatchers = new Lazy<IEnumerable<IRemoteMessageDispatcher>>(components.ResolveAll<IRemoteMessageDispatcher>);
        }

        /// <inheritdoc />
        public virtual async Task Publish(EventMessage instance, ExecutionContext context)
        {
            Argument.NotNull(instance, nameof(instance));

            var request = _requestContext.Value.Resolve(instance, context.Request);
            await _requests.Value.Append(request);

            var endPoints = _services.Value.Find(instance);
            foreach (var endPoint in endPoints)
            {
                if (endPoint.Method.GetParameters().FirstOrDefault()?.ParameterType.AssemblyQualifiedName == instance.MessageType)
                {
                    await _dispatcher.Value.Dispatch(request, endPoint, context);
                }
                else
                {
                    var attribute = endPoint.ServiceType.GetAllAttributes<SubscribeAttribute>().FirstOrDefault();
                    if (attribute != null)
                    {
                        if (attribute.Channel == instance.Name)
                        {
                            await _dispatcher.Value.Dispatch(request, endPoint, context);
                        }
                    }
                }
            }
        }

        /// <inheritdoc />
        public async Task Publish(IEnumerable<EventMessage> instances, ExecutionContext context = null)
        {
            Argument.NotNull(instances, nameof(instances));

            foreach (var item in instances)
            {
                await this.Publish(item, context);
            }
        }

        public void Publish(string channel, string message)
        {
            var instance = JsonConvert.DeserializeObject<JObject>(message);

            var requestId = instance["requestId"].Value<string>();
            var body = instance["body"].ToObject<object>();

            var current = new EventMessage(requestId, body);

            foreach (var endPoint in _services.Value.EndPoints.Where(e => e.ServiceType.GetAllAttributes<SubscribeAttribute>().Any(x => x.Channel == channel)))
            {
                var request = _requestContext.Value.Resolve(current, endPoint);

                _dispatcher.Value.Dispatch(request, endPoint, null);
            }
        }

        /// <inheritdoc />
        public Task<MessageResult> Send(object message, ExecutionContext parentContext = null, TimeSpan? timeout = null)
        {
            return this.Send((string) null, message, parentContext, timeout);
        }

        /// <inheritdoc />
        public virtual async Task<MessageResult> Send(string path, object instance, ExecutionContext parentContext = null, TimeSpan? timeout = null)
        {
            var endPoint = _services.Value.Find(path, instance);
            if (endPoint != null)
            {
                var request = _requestContext.Value.Resolve(instance, endPoint, parentContext?.Request);
                await _requests.Value.Append(request);
                return await _dispatcher.Value.Dispatch(request, endPoint, parentContext, timeout);
            }
            else
            {
                var request = _requestContext.Value.Resolve(path, instance, parentContext?.Request);
                await _requests.Value.Append(request);
                var dispatcher = _dispatchers.Value.FirstOrDefault(e => e.CanDispatch(request));
                if (dispatcher != null)
                {
                    return await dispatcher.Dispatch(request, parentContext, timeout);
                }
            }
            throw new InvalidOperationException("No endpoint could be found for the request.");
        }

        /// <inheritdoc />
        public virtual async Task<MessageResult> Send(string path, string command, ExecutionContext parentContext = null, TimeSpan? timeout = null)
        {
            var endPoint = _services.Value.Find(path);
            if (endPoint != null)
            {
                var request = _requestContext.Value.Resolve(command, endPoint, parentContext?.Request);
                await _requests.Value.Append(request);
                return await _dispatcher.Value.Dispatch(request, endPoint, parentContext, timeout);
            }
            else
            {
                var request = _requestContext.Value.Resolve(path, command, parentContext?.Request);
                await _requests.Value.Append(request);
                var dispatcher = _dispatchers.Value.FirstOrDefault(e => e.CanDispatch(request));
                if (dispatcher != null)
                {
                    return await dispatcher.Dispatch(request, parentContext, timeout);
                }
            }
            throw new InvalidOperationException("No endpoint could be found for the request.");
        }
    }
}