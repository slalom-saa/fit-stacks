using Slalom.Stacks.Services.Inventory;
using Slalom.Stacks.Services.Logging;

namespace Slalom.Stacks.Services.Messaging
{
    /// <summary>
    /// Resolves a request context using the specified parameters.
    /// </summary>
    public interface IRequestContext
    {
        /// <summary>
        /// Resolves the current request.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="endPoint">The endpoint.</param>
        /// <param name="parent">The parent.</param>
        /// <returns>The current request.</returns>
        Request Resolve(object message, EndPointMetaData endPoint, Request parent = null);

        /// <summary>
        /// Resolves the current request.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="endPoint">The endpoint.</param>
        /// <param name="parent">The parent.</param>
        /// <returns>The current request.</returns>
        Request Resolve(string path, EndPointMetaData endPoint, Request parent = null);
        
        /// <summary>
        /// Resolves the current request.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="parent">The parent.</param>
        /// <returns>The current request.</returns>
        Request Resolve(EventMessage instance, Request parent);

        /// <summary>
        /// Resolves the current request.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="message">The message.</param>
        /// <param name="parent">The parent.</param>
        /// <returns>The current request.</returns>
        Request Resolve(string path, object message, Request parent = null);
    }
}