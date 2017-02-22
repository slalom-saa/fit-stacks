using System;
using System.Linq;
using System.Threading.Tasks;

namespace Slalom.Stacks.Messaging.Routing
{
    public class Request
    {
        public string Path { get; private set; }

        public IMessage Message { get; }

        public IRequestHandler Recipient { get; }

        public MessageContext Context { get; }

        public Request(IMessage message, IRequestHandler recipient, MessageContext context)
        {
            this.Message = message;
            this.Recipient = recipient;
            this.Context = context;
        }

        public Task Execute()
        {
            return this.Recipient.Handle(this.Message, this.Context);
        }
    }
}
