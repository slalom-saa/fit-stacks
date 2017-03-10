using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Autofac;
using Newtonsoft.Json;
using Slalom.Stacks.Messaging.Events;
using Slalom.Stacks.Messaging.Logging;
using Slalom.Stacks.Services;
using Slalom.Stacks.Services.Registry;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Messaging
{
    /// <summary>
    /// A default <see cref="IMessageGateway" /> implementation.
    /// </summary>
    public class MessageGateway : IMessageGateway
    {
        private readonly Lazy<IEnumerable<IMessageDispatcher>> _dispatchers;
        private readonly Lazy<IRequestContext> _requestContext;
        private readonly Lazy<IRequestLog> _requests;
        private readonly Lazy<ServiceRegistry> _services;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageGateway" /> class.
        /// </summary>
        /// <param name="components">The components.</param>
        public MessageGateway(IComponentContext components)
        {
            Argument.NotNull(components, nameof(components));

            _services = new Lazy<ServiceRegistry>(components.Resolve<ServiceRegistry>);
            _requestContext = new Lazy<IRequestContext>(components.Resolve<IRequestContext>);
            _requests = new Lazy<IRequestLog>(components.Resolve<IRequestLog>);
            _dispatchers = new Lazy<IEnumerable<IMessageDispatcher>>(components.ResolveAll<IMessageDispatcher>);
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
                _dispatchers.Value.ToList().ForEach(e => e.Dispatch(request, endPoint, context));
            }
            _dispatchers.Value.ToList().ForEach(e => e.Dispatch(request, context));
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

        /// <inheritdoc />
        public Task<MessageResult> Send(Command instance, ExecutionContext parentContext = null, TimeSpan? timeout = null)
        {
            return this.Send((string) null, (Command) instance, parentContext, timeout);
        }

        /// <inheritdoc />
        public virtual async Task<MessageResult> Send(string path, Command instance, ExecutionContext parentContext = null, TimeSpan? timeout = null)
        {
            var endPoint = _services.Value.Find(path, instance);
            if (endPoint == null)
            {
                throw new InvalidOperationException("No endpoint could be found for the request.");
            }

            var request = _requestContext.Value.Resolve(instance, endPoint, parentContext?.Request);
            //await _requests.Value.Append(request);

            var dispatch = this.GetDispatchers(endPoint).FirstOrDefault();
            if (dispatch == null)
            {
                throw new InvalidOperationException("No dipatcher could be found that could handle the endpoint.");
            }

            return await dispatch.Dispatch(request, endPoint, parentContext);
        }

        /// <inheritdoc />
        public virtual async Task<MessageResult> Send(string path, string command, ExecutionContext parentContext = null, TimeSpan? timeout = null)
        {
            var endPoint = _services.Value.Find(path);
            if (endPoint == null)
            {
                throw new InvalidOperationException("No endpoint could be found for the request.");
            }

            var request = _requestContext.Value.Resolve(command, endPoint, parentContext?.Request);
            //await _requests.Value.Append(request);

            var dispatch = this.GetDispatchers(endPoint).FirstOrDefault();
            if (dispatch == null)
            {
                throw new InvalidOperationException("No dipatcher could be found that could handle the endpoint.");
            }

            return await dispatch.Dispatch(request, endPoint, parentContext);
        }

        protected virtual IEnumerable<IMessageDispatcher> GetDispatchers(EndPointMetaData endPoint)
        {
            return _dispatchers.Value.Where(e => e.CanDispatch(endPoint));
        }
    }
}