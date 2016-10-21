using System;
using System.Security.Claims;

namespace Slalom.FitStacks.Runtime
{
    /// <summary>
    /// Represents a local execution context.  Meant to be used with console apps and testing.
    /// </summary>
    /// <seealso cref="Slalom.FitStacks.Runtime.ExecutionContext" />
    public class LocalExecutionContext : ExecutionContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalExecutionContext"/> class.
        /// </summary>
        public LocalExecutionContext()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalExecutionContext"/> class.
        /// </summary>
        /// <param name="userName">The current user's username.</param>
        /// <param name="claims">The user's claims.</param>
        public LocalExecutionContext(string userName, params Claim[] claims)
            : base(userName, claims)
        {
        }
    }
}