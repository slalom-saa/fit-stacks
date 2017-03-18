using System;
using Autofac;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Slalom.Stacks.Messaging.Events;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Services;
using Slalom.Stacks.Services.Registry;

namespace Slalom.Stacks.Messaging
{
    /// <summary>
    /// A local <see cref="IMessageDispatcher"/> implementation.
    /// </summary>
    /// <seealso cref="Slalom.Stacks.Messaging.IMessageDispatcher" />
    public class MessageDispatcher : IMessageDispatcher
    {
        private readonly IComponentContext _components;
        private readonly IEnvironmentContext _environmentContext;
        private IRequestContext _requestContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageDispatcher"/> class.
        /// </summary>
        /// <param name="components">The configured <see cref="IComponentContext"/>.</param>
        public MessageDispatcher(IComponentContext components)
        {
            _components = components;
            _environmentContext = components.Resolve<IEnvironmentContext>();
            _requestContext = components.Resolve<IRequestContext>();
        }

        /// <inheritdoc />
        public bool CanDispatch(EndPointMetaData endPoint)
        {
            return endPoint.RootPath == ServiceHost.LocalPath;
        }

        /// <inheritdoc />
        public async Task<MessageResult> Dispatch(Request request, ExecutionContext context)
        {
            context = new ExecutionContext(request, context);

            var handlers = _components.ResolveAll(typeof(IHandle<>).MakeGenericType(request.Message.MessageType));
            foreach (var handler in handlers)
            {
                typeof(IHandle<>).MakeGenericType(request.Message.MessageType).GetMethod("Handle").Invoke(handler, new[] { request.Message.Body });
            }

            return new MessageResult(context);
        }

        /// <inheritdoc />
        public async Task<MessageResult> Dispatch(Request request, EndPointMetaData endPoint, ExecutionContext parentContext, TimeSpan? timeout = null)
        {
            CancellationTokenSource source;
            if (timeout.HasValue || endPoint.Timeout.HasValue)
            {
                source = new CancellationTokenSource(timeout ?? endPoint.Timeout.Value);
            }
            else
            {
                source = new CancellationTokenSource();
            }

            var context = new ExecutionContext(request, endPoint, source.Token, parentContext);

            var handler = _components.Resolve(Type.GetType(endPoint.ServiceType));
            var service = handler as IService;
            if (service != null)
            {
                service.Context = context;
            }
            
            await (Task)endPoint.EndPointMethod.Invoke(handler, new object[] { request.Message.Body });

            return new MessageResult(context);
        }
    }
}