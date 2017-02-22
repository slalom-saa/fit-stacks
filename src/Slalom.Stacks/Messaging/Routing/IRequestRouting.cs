using System.Collections.Generic;

namespace Slalom.Stacks.Messaging.Routing
{
    public interface IRequestRouting
    {
        IEnumerable<Request> BuildRequests(IMessage command);
    }
}