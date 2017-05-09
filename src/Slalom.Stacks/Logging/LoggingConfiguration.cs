/* 
 * Copyright (c) Stacks Contributors
 * 
 * This file is subject to the terms and conditions defined in
 * the LICENSE file, which is part of this source code package.
 */

using Autofac;

namespace Slalom.Stacks.Logging
{
    /// <summary>
    /// Contains methods to build stacks.
    /// </summary>
    public static class LoggingConfiguration
    {
        /// <summary>
        /// Adds console logging to the stack.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The current instance for method chaining.</returns>
        public static Stack UseConsoleLogging(this Stack instance)
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