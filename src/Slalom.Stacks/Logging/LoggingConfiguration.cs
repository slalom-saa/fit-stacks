using Autofac;

namespace Slalom.Stacks.Logging
{
    /// <summary>
    /// Contains methods to build stacks.
    /// </summary>
    public static class LoggingConfiguration
    {
        /// <summary>
        /// Adds simple console logging to the stack.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The current instance for method chaining.</returns>
        public static Stack UseSimpleConsoleLogging(this Stack instance)
        {
            instance.Use(builder =>
            {
                builder.Register(c => new SimpleConsoleLogger())
                    .PreserveExistingDefaults()
                    .SingleInstance()
                    .As<ILogger>();
            });

            return instance;
        }
    }
}