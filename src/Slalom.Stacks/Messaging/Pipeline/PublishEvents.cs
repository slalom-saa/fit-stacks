using System;
using System.Linq;
using System.Threading.Tasks;
using Autofac;

namespace Slalom.Stacks.Messaging.Pipeline
{
    public class PublishEvents : IMessageExecutionStep
    {
        private IMessageRouter _eventRouter;

        public PublishEvents(IComponentContext context)
        {
            _eventRouter = context.Resolve<IMessageRouter>();
        }

        public Task Execute(IMessage message, MessageExecutionContext context)
        {
            if (context.IsSuccessful)
            {
                _eventRouter.Publish(context.RaisedEvents, context);
                if (context.Response is IEvent)
                {
                    _eventRouter.Publish((IEvent)context.Response, context);
                }
            }
            return Task.FromResult(0);
        }
    }
}
