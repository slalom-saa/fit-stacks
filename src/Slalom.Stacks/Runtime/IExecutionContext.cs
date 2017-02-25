namespace Slalom.Stacks.Runtime
{
    /// <summary>
    /// Contains a method to resolve the current execution context.
    /// </summary>
    public interface IExecutionContext
    {
        /// <summary>
        /// Resolves the current execution context.
        /// </summary>
        /// <returns>The current execution context.</returns>
        ExecutionContext Resolve();
    }
}