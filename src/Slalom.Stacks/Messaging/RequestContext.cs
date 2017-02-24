using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slalom.Stacks.Messaging
{
    public class RequestContext
    {
        public string RequestName { get; }
        public string Path { get; }
        public IMessage Message { get; }

        public RequestContext(string requestName, string path, IMessage message)
        {
            this.RequestName = requestName;
            this.Path = path;
            this.Message = message;
            this.RequestId = message.Id;
        }

        public string RequestId { get; }
    }
}
