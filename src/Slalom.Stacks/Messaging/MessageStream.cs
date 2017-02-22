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
        private Lazy<IExecutionContextResolver> _executionContext;
        private Lazy<IRequestRouting> _routing;

        public MessageStream(IComponentContext components)
        {
            _executionContext = new Lazy<IExecutionContextResolver>(components.Resolve<IExecutionContextResolver>);
            _routing = new Lazy<IRequestRouting>(components.Resolve<IRequestRouting>);
        }

        public async Task<MessageResult> Send(ICommand instance, MessageContext context = null, TimeSpan? timeout = null)
        {
            var requests = _routing.Value.BuildRequests(instance).ToList();
            if (requests.Count() != 1)
            {
                throw new Exception("TBD");
            }

            context = new MessageContext(requests.Single(), _executionContext.Value.Resolve(), context);

            await requests.First().Recipient.Handle(instance, context);

            return new MessageResult(context);
        }


        public async Task Publish(IEvent instance, MessageContext context = null)
        {
            var requests = _routing.Value.BuildRequests(instance);

            foreach (var request in requests)
            {
                context = new MessageContext(request, _executionContext.Value.Resolve(), context);

                await request.Recipient.Handle(instance, context);
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