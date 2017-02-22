using System.Collections.Generic;
using Autofac;
using Slalom.Stacks.Runtime;

namespace Slalom.Stacks.Messaging.Routing
{
    public class RequestRouting : IRequestRouting
    {
        private readonly IComponentContext _components;

        public RequestRouting(IComponentContext components)
        {
            _components = components;
        }

        public IEnumerable<Request> BuildRequests(IMessage command, MessageContext parent = null)
        {
            var executionContext = _components.Resolve<IExecutionContextResolver>().Resolve();;
            var handlers = _components.ResolveAll(typeof(IHandle<>).MakeGenericType(command.GetType()));
            foreach (var item in handlers)
            {
                var context = new MessageContext(command.Id, item.GetType().Name, null, executionContext, parent);

                yield return new Request(command, new IHandleRequestHandler(item), context);
            }
        }
    }
}