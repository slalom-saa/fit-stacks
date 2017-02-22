using System.Reflection;
using System.Threading.Tasks;

namespace Slalom.Stacks.Messaging.Routing
{
    public class RequestHandlerReference : IRequestHandler, IUseMessageContext
    {
        private readonly object _instance;

        public RequestHandlerReference(object instance)
        {
            _instance = instance;
        }

        public Task Handle(object instance)
        {
            if (_instance is IUseMessageContext)
            {
                ((IUseMessageContext) _instance).SetContext(this.Context);
            }
            return (Task)typeof(IHandle<>).MakeGenericType(instance.GetType()).GetMethod("Handle").Invoke(_instance, new[] { instance });
        }

        public void SetContext(MessageContext context)
        {
            this.Context = context;
        }

        public MessageContext Context { get; private set; }
    }
}