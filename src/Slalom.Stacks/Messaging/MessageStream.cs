using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Slalom.Stacks.Messaging.Pipeline;
using Slalom.Stacks.Messaging.Routing;
using Slalom.Stacks.Runtime;

namespace Slalom.Stacks.Messaging
{
    public class MessageStream : IMessageStream
    {
        private Lazy<IRequestRouting> _routing;

        public MessageStream(IComponentContext components)
        {
            _routing = new Lazy<IRequestRouting>(components.Resolve<IRequestRouting>);
        }

        public async Task<MessageResult> Send(ICommand instance, MessageContext context = null, TimeSpan? timeout = null)
        {
            var requests = _routing.Value.BuildRequests(instance, context).ToList();
            if (requests.Count() != 1)
            {
                throw new Exception("TBD");
            }

            await requests.First().Execute();

            return new MessageResult(requests.First());
        }


        public async Task Publish(IEvent instance, MessageContext context = null)
        {
            var requests = _routing.Value.BuildRequests(instance, context);

            foreach (var request in requests)
            {
                await request.Execute();
            }
        }

        public async Task Publish(IEnumerable<IEvent> instance, MessageContext context = null)
        {
            foreach (var item in instance)
            {
                await this.Publish(item, context);
            }
        }
    }
}