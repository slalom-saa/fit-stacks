using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Slalom.Stacks.Messaging.Logging;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Services;
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
        private readonly Lazy<IEnumerable<IRequestStore>> _requests;
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
            _requests = new Lazy<IEnumerable<IRequestStore>>(components.ResolveAll<IRequestStore>);
            _dispatchers = new Lazy<IEnumerable<IMessageDispatcher>>(components.ResolveAll<IMessageDispatcher>);
        }


        /// <inheritdoc />
        public async Task Publish(IEvent instance, MessageExecutionContext parentContext = null)
        {
            Argument.NotNull(instance, nameof(instance));

            var request = _requestContext.Value.Resolve(null, instance, parentContext?.Request);
            await Task.WhenAll(_requests.Value.Select(e => e.Append(new RequestEntry(request))));

            var endPoints = _services.Value.Find(instance).ToList();

            foreach (var endPoint in endPoints)
            {
                foreach (var dispatcher in _dispatchers.Value.Where(e => e.CanDispatch(endPoint)))
                {
                    await dispatcher.Dispatch(request, endPoint, parentContext);
                }
            }
        }

        /// <inheritdoc />
        public async Task Publish(IEnumerable<IEvent> instances, MessageExecutionContext context = null)
        {
            Argument.NotNull(instances, nameof(instances));

            foreach (var item in instances)
            {
                await this.Publish(item, context);
            }
        }

        /// <inheritdoc />
        public Task<MessageResult> Send(ICommand instance, MessageExecutionContext parentContext = null, TimeSpan? timeout = null)
        {
            return this.Send(null, instance, parentContext, timeout);
        }

        /// <inheritdoc />
        public async Task<MessageResult> Send(string path, ICommand instance, MessageExecutionContext parentContext = null, TimeSpan? timeout = null)
        {
            var request = _requestContext.Value.Resolve(path, instance, parentContext?.Request);
            await Task.WhenAll(_requests.Value.Select(e => e.Append(new RequestEntry(request))));

            var endPoint = _services.Value.Find(path, instance);
            if (endPoint == null)
            {
                throw new Exception("TBD");
            }

            var dispatch = _dispatchers.Value.FirstOrDefault(e => e.CanDispatch(endPoint));
            if (dispatch == null)
            {
                throw new Exception("TBD");
            }

            return await dispatch.Dispatch(request, endPoint, parentContext);
        }

        /// <inheritdoc />
        public async Task<MessageResult> Send(string path, string command, MessageExecutionContext parentContext = null, TimeSpan? timeout = null)
        {
            if (string.IsNullOrWhiteSpace(command))
            {
                command = "{}";
            }

            var endPoint = _services.Value.Find(path);
            if (endPoint == null)
            {
                throw new Exception("TBD");
            }

            var request = _requestContext.Value.Resolve(path, endPoint.CreateMessage(command), parentContext?.Request);
            await Task.WhenAll(_requests.Value.Select(e => e.Append(new RequestEntry(request))));

            var dispatch = _dispatchers.Value.FirstOrDefault(e => e.CanDispatch(endPoint));
            if (dispatch == null)
            {
                throw new Exception("TBD");
            }

            return await dispatch.Dispatch(request, endPoint, parentContext);
        }
    }
}