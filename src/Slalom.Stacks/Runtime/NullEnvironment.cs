/* 
 * Copyright (c) Stacks Contributors
 * 
 * This file is subject to the terms and conditions defined in
 * the LICENSE file, which is part of this source code package.
 */

namespace Slalom.Stacks.Runtime
{
    /// <summary>
    /// Represents an execution request.  Intended to implement the null object pattern.
    /// </summary>
    /// <seealso cref="Environment" />
    /// <seealso href="http://bit.ly/29e2gRR">Wikipedia: Null Object pattern</seealso>
    public class NullEnvironment : Environment
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NullEnvironment" /> class.
        /// </summary>
        public NullEnvironment()
            : base("", "", "", 0)
        {
        }
    }
}