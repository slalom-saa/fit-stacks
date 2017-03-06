namespace Slalom.Stacks.Messaging
{
    /// <summary>
    /// Contains a method to resolve the current request context.
    /// </summary>
    public interface IRequestContext
    {
        /// <summary>
        /// Resolves the current request context.
        /// </summary>
        /// <param name="path">The request path.</param>
        /// <param name="message">The request message.</param>
        /// <param name="parent">The parent context.</param>
        /// <returns>Returns the current request context.</returns>
        Request Resolve(string path, object message, Request parent = null);
    }
}