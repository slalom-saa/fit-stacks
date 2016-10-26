namespace Slalom.Stacks.Messaging
{
    /// <summary>
    /// Decorator to events that should return a message.  Typically events do not return a message and require the user
    /// to already know how to make subsequent calls.  A common use is to return the ID of a newly created aggregate.
    /// </summary>
    public interface IHaveReturnMessage
    {
        /// <summary>
        /// Gets the message that should be returned to the requestor.
        /// </summary>
        /// <returns>The message that should be returned to the requestor.</returns>
        dynamic GetReturnMessage();
    }
}