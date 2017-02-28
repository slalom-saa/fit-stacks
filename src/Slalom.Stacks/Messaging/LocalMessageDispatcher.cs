using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Services;

namespace Slalom.Stacks.Messaging
{
    public class LocalMessageDispatcher : IMessageDispatcher
    {
        private readonly IComponentContext _components;
        private readonly IExecutionContext _executionContext;

        public LocalMessageDispatcher(IComponentContext components)
        {
            _components = components;
            _executionContext = components.Resolve<IExecutionContext>();
        }

        public async Task<MessageResult> Dispatch(RequestContext request, EndPoint endPoint, MessageExecutionContext parentContext)
        {
            var executionContext = _executionContext.Resolve();
            var handler = _components.Resolve(Type.GetType(endPoint.Type));

            var context = new MessageExecutionContext(request, endPoint, executionContext, parentContext);

            (handler as IUseMessageContext)?.UseContext(context);

            await(Task) handler.GetType().GetMethod("Handle").Invoke(handler, new object[] { request.Message });

            return new MessageResult(context);
        }

        public bool CanDispatch(EndPoint endPoint)
        {
            return endPoint.RootPath == Service.LocalPath;
        }
    }

    public interface IMessageDispatcher
    {
        Task<MessageResult> Dispatch(RequestContext request, EndPoint endPoint, MessageExecutionContext parentContext);
        bool CanDispatch(EndPoint endPoint);
    }
}
