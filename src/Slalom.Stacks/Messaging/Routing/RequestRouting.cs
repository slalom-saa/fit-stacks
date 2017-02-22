using System.Collections.Generic;
using Autofac;

namespace Slalom.Stacks.Messaging.Routing
{
    public class RequestRouting : IRequestRouting
    {
        private readonly IComponentContext _components;

        public RequestRouting(IComponentContext components)
        {
            _components = components;
        }

        public IEnumerable<Request> BuildRequests(IMessage command)
        {
            var handlers = _components.ResolveAll(typeof(IHandle<>).MakeGenericType(command.GetType()));
            foreach (var item in handlers)
            {
                yield return new Request(item.GetType().Name, command, new RequestHandlerReference(item));
            }
        }
    }
}