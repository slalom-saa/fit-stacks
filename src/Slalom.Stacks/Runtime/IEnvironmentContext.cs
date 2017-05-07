/* 
 * Copyright (c) Stacks Contributors
 * 
 * This file is subject to the terms and conditions defined in
 * the LICENSE file, which is part of this source code package.
 */

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