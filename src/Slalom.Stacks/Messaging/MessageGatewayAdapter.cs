using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Newtonsoft.Json;
using Slalom.Stacks.Messaging.Logging;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Services;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Messaging
{
    /// <summary>
    /// A default <see cref="IMessageGatewayAdapter" /> implementation.
    /// </summary>
    public class MessageGatewayAdapter : IMessageGatewayAdapter
    {
        private readonly IComponentContext _components;
        private readonly Lazy<ServiceRegistry> _registry;
        private readonly Lazy<IRequestContext> _requestContext;
        private readonly Lazy<IEnumerable<IRequestStore>> _requests;
        private readonly Lazy<IExecutionContext> _executionContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageGatewayAdapter" /> class.
        /// </summary>
        /// <param name="components">The components.</param>
        public MessageGatewayAdapter(IComponentContext components)
        {
            Argument.NotNull(components, nameof(components));

            _components = components;

            _registry = new Lazy<ServiceRegistry>(components.Resolve<ServiceRegistry>);
            _requestContext = new Lazy<IRequestContext>(components.Resolve<IRequestContext>);
            _requests = new Lazy<IEnumerable<IRequestStore>>(components.ResolveAll<IRequestStore>);
            _executionContext = new Lazy<IExecutionContext>(components.Resolve<IExecutionContext>);
        }


        /// <inheritdoc />
        public async Task Publish(IEvent instance, MessageExecutionContext parentContext = null)
        {
            Argument.NotNull(instance, nameof(instance));

            var request = _requestContext.Value.Resolve(null, instance, parentContext?.RequestContext);
            await Task.WhenAll(_requests.Value.Select(e => e.Append(new RequestEntry(request))));

            var entries = _registry.Value.Find(instance).ToList();

            foreach (var entry in entries)
            {
                var handler = _components.Resolve(Type.GetType(entry.Type));
                var executionContext = _executionContext.Value.Resolve();

                var context = new MessageExecutionContext(request, entry, executionContext, parentContext);

                (handler as IUseMessageContext)?.UseContext(context);

                await (Task) handler.GetType().GetMethod("Handle").Invoke(handler, new object[] {instance});
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
            var request = _requestContext.Value.Resolve(path, instance, parentContext?.RequestContext);
            await Task.WhenAll(_requests.Value.Select(e => e.Append(new RequestEntry(request))));

            var entries = _registry.Value.Find(instance).ToList();
            if (entries.Count() != 1)
            {
                throw new Exception("TBD");
            }

            var entry = entries.First();

            var handler = _components.Resolve(Type.GetType(entry.Type));
            var executionContext = _executionContext.Value.Resolve();

            var context = new MessageExecutionContext(request, entry, executionContext, parentContext);

            (handler as IUseMessageContext)?.UseContext(context);

            await (Task) handler.GetType().GetMethod("Handle").Invoke(handler, new object[] {instance});

            return new MessageResult(context);
        }

        /// <inheritdoc />
        public async Task<MessageResult> Send(string path, string command, MessageExecutionContext parentContext = null, TimeSpan? timeout = null)
        {
            var entry = _registry.Value.Find(path);
            if (String.IsNullOrWhiteSpace(command))
            {
                command = "{}";
            }
            var instance = (ICommand) JsonConvert.DeserializeObject(command, Type.GetType(entry.RequestType));

            var request = _requestContext.Value.Resolve(path, instance, parentContext?.RequestContext);
            await Task.WhenAll(_requests.Value.Select(e => e.Append(new RequestEntry(request))));

            var handler = _components.Resolve(Type.GetType(entry.Type));
            var executionContext = _executionContext.Value.Resolve();

            var context = new MessageExecutionContext(request, entry, executionContext, parentContext);

            (handler as IUseMessageContext)?.UseContext(context);

            await (Task) handler.GetType().GetMethod("Handle").Invoke(handler, new object[] {instance});

            return new MessageResult(context);
        }
    }
}