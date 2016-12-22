namespace Slalom.Stacks.Runtime
{
    /// <summary>
    /// Allows access to the execution context by reference to this interface.
    /// </summary>
    public interface IHaveExecutionContext
    {
        /// <summary>
        /// Gets or sets the current <see cref="ExecutionContext"/>.
        /// </summary>
        /// <value>The current <see cref="ExecutionContext"/>.</value>
        ExecutionContext Context { get; }
    }
}