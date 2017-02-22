using System;
using System.Linq;

namespace Slalom.Stacks.Messaging.Routing
{
    public class Request
    {
        public string Name { get; set; }

        public string Path { get; private set; }

        public IMessage Message { get; }

        public IRequestHandler Recipient { get; }

        public Request(string name, IMessage message, IRequestHandler recipient)
        {
            this.Name = name;
            this.Message = message;
            this.Recipient = recipient;
        }
    }
}
