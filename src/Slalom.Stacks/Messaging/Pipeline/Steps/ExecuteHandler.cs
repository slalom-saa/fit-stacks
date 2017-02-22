using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;

namespace Slalom.Stacks.Messaging.Pipeline.Steps
{
    public class ExecuteHandler : IMessageExecutionStep
    {
        private readonly IComponentContext _context;

        public ExecuteHandler(IComponentContext context)
        {
            _context = context;
        }

        public async Task Execute(IMessage message, MessageContext context)
        {
            if (!context.ValidationErrors.Any())
            {
                try
                {
                    var instance = context.Request.Recipient;
                    if (instance is IUseMessageContext)
                    {
                        ((IUseMessageContext) instance).SetContext(context);
                    }
                    await instance.Handle(message);
                    if (context.Response is Event)
                    {
                        context.AddRaisedEvent((Event) context.Response);
                    }
                }
                catch (Exception exception)
                {
                    context.RaiseException(exception);
                }
            }
        }
    }
}
