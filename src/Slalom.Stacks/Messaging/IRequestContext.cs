using Slalom.Stacks.Services.Registry;

namespace Slalom.Stacks.Messaging
{
    public interface IRequestContext
    {
        Request Resolve(object message, EndPointMetaData endPoint, Request parent = null);
    }
}