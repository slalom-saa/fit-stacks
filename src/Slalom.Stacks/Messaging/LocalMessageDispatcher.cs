using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Newtonsoft.Json;
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

            var message = request.Message;
            if (message.GetType().AssemblyQualifiedName != endPoint.RequestType)
            {
                if (message is string)
                {
                    message = (IMessage)JsonConvert.DeserializeObject((string)message, Type.GetType(endPoint.RequestType));
                }
                else
                {
                    message = (IMessage) JsonConvert.DeserializeObject(JsonConvert.SerializeObject(message), Type.GetType(endPoint.RequestType));
                }
            }

            (handler as IUseMessageContext)?.UseContext(context);

            await (Task)handler.GetType().GetMethod("Handle").Invoke(handler, new object[] { message });

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
