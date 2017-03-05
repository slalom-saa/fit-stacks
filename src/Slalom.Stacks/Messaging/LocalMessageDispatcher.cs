using System;
using Autofac;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Services;

namespace Slalom.Stacks.Messaging
{
    /// <summary>
    /// A local <see cref="IMessageDispatcher"/> implementation.
    /// </summary>
    /// <seealso cref="Slalom.Stacks.Messaging.IMessageDispatcher" />
    public class LocalMessageDispatcher : IMessageDispatcher
    {
        private readonly IComponentContext _components;
        private readonly IExecutionContext _executionContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalMessageDispatcher"/> class.
        /// </summary>
        /// <param name="components">The configured <see cref="IComponentContext"/>.</param>
        public LocalMessageDispatcher(IComponentContext components)
        {
            _components = components;
            _executionContext = components.Resolve<IExecutionContext>();
        }

        /// <inheritdoc />
        public bool CanDispatch(EndPoint endPoint)
        {
            return endPoint.RootPath == Service.LocalPath;
        }

        /// <inheritdoc />
        public async Task<MessageResult> Dispatch(RequestContext request, EndPoint endPoint, MessageExecutionContext parentContext, TimeSpan? timeout = null)
        {
            var executionContext = _executionContext.Resolve();
            var handler = _components.Resolve(Type.GetType(endPoint.Type));

            CancellationTokenSource source;
            if (timeout.HasValue || endPoint.Timeout.HasValue)
            {
                source = new CancellationTokenSource(timeout ?? endPoint.Timeout.Value);
            }
            else
            {
                source = new CancellationTokenSource();
            }

            var context = new MessageExecutionContext(request, endPoint, executionContext, source.Token, parentContext);

            (handler as IUseMessageContext)?.UseContext(context);

            await (Task)typeof(IHandle).GetMethod("Handle").Invoke(handler, new object[] { request.Message });

            return new MessageResult(context);
        }
    }
}