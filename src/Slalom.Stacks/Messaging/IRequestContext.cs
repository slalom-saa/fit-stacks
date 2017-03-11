using Slalom.Stacks.Messaging.Events;
using Slalom.Stacks.Services.Registry;

namespace Slalom.Stacks.Messaging
{
    public interface IRequestContext
    {
        Request Resolve(Command command, EndPointMetaData endPoint, Request parent = null);
        Request Resolve(string command, EndPointMetaData endPoint, Request parent = null);
        Request Resolve(EventMessage instance, Request parent);
    }
}