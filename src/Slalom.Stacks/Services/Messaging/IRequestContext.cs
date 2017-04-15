using Slalom.Stacks.Services.Inventory;

namespace Slalom.Stacks.Services.Messaging
{
    public interface IRequestContext
    {
        Request Resolve(object message, EndPointMetaData endPoint, Request parent = null);
        Request Resolve(string path, EndPointMetaData endPoint, Request parent = null);
        Request Resolve(EventMessage instance, Request parent);

        /// <inheritdoc />
        Request Resolve(string path, object message, Request parent = null);
    }
}