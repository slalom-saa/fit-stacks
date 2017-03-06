using System;
using Autofac;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Services;
using Slalom.Stacks.Services.Registry;

namespace Slalom.Stacks.Messaging
{
    /// <summary>
    /// A local <see cref="IMessageDispatcher"/> implementation.
    /// </summary>
    /// <seealso cref="Slalom.Stacks.Messaging.IMessageDispatcher" />
    public class LocalMessageDispatcher : IMessageDispatcher
    {
        private readonly IComponentContext _components;
        private readonly IEnvironmentContext _environmentContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalMessageDispatcher"/> class.
        /// </summary>
        /// <param name="components">The configured <see cref="IComponentContext"/>.</param>
        public LocalMessageDispatcher(IComponentContext components)
        {
            _components = components;
            _environmentContext = components.Resolve<IEnvironmentContext>();
        }

        /// <inheritdoc />
        public bool CanDispatch(EndPointMetaData endPoint)
        {
            return endPoint.RootPath == ServiceHost.LocalPath;
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
            var service = handler as Service;
            if (service != null)
            {
                service.Request = request;
                service.Context = context;
            }

            var message = request.Message.Body;
            if (message == null)
            {
                message = JsonConvert.DeserializeObject("{}", Type.GetType(endPoint.RequestType));
            }
            else if (message.GetType().AssemblyQualifiedName != endPoint.RequestType)
            {
                if (message is String)
                {
                    message = JsonConvert.DeserializeObject((string)message, Type.GetType(endPoint.RequestType));
                }
            }

            await (Task)typeof(IHandle<>).MakeGenericType(Type.GetType(endPoint.RequestType)).GetMethod("Handle").Invoke(handler, new object[] { message });

            return new MessageResult(context);
        }
    }
}