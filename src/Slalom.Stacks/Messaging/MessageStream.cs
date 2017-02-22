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
        private Lazy<IMessageExecutionPipeline> _pipe;
        private Lazy<IExecutionContextResolver> _executionContext;
        private Lazy<IRequestRouting> _routing;

        public MessageStream(IComponentContext components)
        {
            _pipe = new Lazy<IMessageExecutionPipeline>(components.Resolve<IMessageExecutionPipeline>);
            _executionContext = new Lazy<IExecutionContextResolver>(components.Resolve<IExecutionContextResolver>);
            _routing = new Lazy<IRequestRouting>(components.Resolve<IRequestRouting>);
        }

        public async Task<MessageResult> Send(ICommand command, MessageContext context = null, TimeSpan? timeout = null)
        {
            var requests = _routing.Value.BuildRequests(command).ToList();
            if (requests.Count() != 1)
            {
                throw new Exception("TBD");
            }

            context = new MessageContext(requests.Single(), _executionContext.Value.Resolve(), context);
            await _pipe.Value.Execute(command, context);

            return new MessageResult(context);
        }


        public async Task Publish(IEvent command, MessageContext context = null)
        {
            var requests = _routing.Value.BuildRequests(command);

            foreach (var request in requests)
            {
                context = new MessageContext(request, _executionContext.Value.Resolve(), context);

                await _pipe.Value.Execute(command, context);
            }
        }

        public async Task Publish(IEnumerable<IEvent> command, MessageContext context = null)
        {
            foreach (var item in command)
            {
                await this.Publish(item, context);
            }
        }
    }
}