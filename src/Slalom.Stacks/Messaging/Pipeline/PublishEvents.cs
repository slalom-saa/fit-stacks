using System;
using System.Linq;
using System.Threading.Tasks;
using Autofac;

namespace Slalom.Stacks.Messaging.Pipeline
{
    public class PublishEvents : IMessageExecutionStep
    {
        private IMessageDispatcher _eventDispatcher;

        public PublishEvents(IComponentContext context)
        {
            _eventDispatcher = context.Resolve<IMessageDispatcher>();
        }

        public Task Execute(IMessage message, MessageExecutionContext context)
        {
            if (context.IsSuccessful)
            {
                _eventDispatcher.Publish(context.RaisedEvents, context);
                if (context.Response is IEvent)
                {
                    _eventDispatcher.Publish((IEvent)context.Response, context);
                }
            }
            return Task.FromResult(0);
        }
    }
}
