namespace Slalom.Stacks.Runtime
{
    /// <summary>
    /// Contains a method to resolve the current execution context.
    /// </summary>
    public interface IEnvironmentContext
    {
        /// <summary>
        /// Resolves the current execution context.
        /// </summary>
        /// <returns>The current execution context.</returns>
        Environment Resolve();
    }
}