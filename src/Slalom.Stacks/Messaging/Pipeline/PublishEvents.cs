using System;
using System.Linq;
using System.Threading.Tasks;
using Autofac;

namespace Slalom.Stacks.Messaging.Pipeline
{
    public class PublishEvents : IMessageExecutionStep
    {
        private IMessageStream _eventStream;

        public PublishEvents(IComponentContext context)
        {
            _eventStream = context.Resolve<IMessageStream>();
        }

        public Task Execute(IMessage message, MessageContext context)
        {
            if (context.IsSuccessful)
            {
                _eventStream.Publish(context.RaisedEvents, context);
                if (context.Response is IEvent)
                {
                    _eventStream.Publish((IEvent)context.Response, context);
                }
            }
            return Task.FromResult(0);
        }
    }
}
