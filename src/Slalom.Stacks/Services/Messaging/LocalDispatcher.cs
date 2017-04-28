using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Slalom.Stacks.Services.Inventory;
using Slalom.Stacks.Services.Pipeline;

namespace Slalom.Stacks.Services.Messaging
{
    /// <summary>
    /// A local <see cref="ILocalMessageDispatcher"/> implementation.
    /// </summary>
    /// <seealso cref="ILocalMessageDispatcher" />
    public class LocalDispatcher : ILocalMessageDispatcher
    {
        private readonly IComponentContext _components;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalDispatcher"/> class.
        /// </summary>
        /// <param name="components">The configured <see cref="IComponentContext"/>.</param>
        public LocalDispatcher(IComponentContext components)
        {
            _components = components;
        }

        protected virtual async Task Complete(ExecutionContext context)
        {
            var steps = new List<IMessageExecutionStep>
            {
                _components.Resolve<HandleException>(),
                _components.Resolve<Complete>(),
                _components.Resolve<PublishEvents>(),
                _components.Resolve<LogCompletion>()
            };
            foreach (var step in steps)
            {
                await step.Execute(context);
            }
        }

        /// <inheritdoc />
        public virtual async Task<MessageResult> Dispatch(Request request, ExecutionContext context)
        {
            var handlers = _components.ResolveAll(typeof(IHandle<>).MakeGenericType(request.Message.MessageType));
            foreach (var handler in handlers)
            {
                context = new ExecutionContext(request, context);

                typeof(IHandle<>).MakeGenericType(request.Message.MessageType).GetMethod("Handle").Invoke(handler, new[] { request.Message.Body });

                await this.Complete(context);
            }
            return new MessageResult(context ?? new ExecutionContext(request, null));
        }

        /// <inheritdoc />
        public virtual async Task<MessageResult> Dispatch(Request request, EndPointMetaData endPoint, ExecutionContext parentContext, TimeSpan? timeout = null)
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

            var handler = _components.Resolve(endPoint.ServiceType);
            var service = handler as IEndPoint;
            if (service != null)
            {
                service.Context = context;
            }
            
            await (Task)endPoint.Method.Invoke(handler, new object[] { request.Message.Body });

            await this.Complete(context);

            return new MessageResult(context);
        }
    }
}