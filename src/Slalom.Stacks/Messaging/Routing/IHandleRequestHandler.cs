using System.Reflection;
using System.Threading.Tasks;

namespace Slalom.Stacks.Messaging.Routing
{
    public class IHandleRequestHandler : IRequestHandler
    {
        private readonly object _instance;

        public IHandleRequestHandler(object instance)
        {
            _instance = instance;
        }

        public Task Handle(object instance, MessageContext context)
        {
            if (_instance is IUseMessageContext)
            {
                ((IUseMessageContext) _instance).UseContext(context);
            }
            return (Task)typeof(IHandle<>).MakeGenericType(instance.GetType()).GetMethod("Handle").Invoke(_instance, new[] { instance });
        }
    }
}