namespace Slalom.Stacks.Messaging
{
    /// <summary>
    /// Contract for a class that expects execution context.
    /// </summary>
    public interface IUseExecutionContext
    {
        /// <summary>
        /// Sets the execution context to use.
        /// </summary>
        /// <param name="context">The execution context to use.</param>
        void UseContext(ExecutionContext context);
    }
}