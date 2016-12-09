using System;
using System.Security.Claims;

namespace Slalom.Stacks.Runtime
{
    /// <summary>
    /// Represents a local execution context.  Meant to be used with console apps and testing.
    /// </summary>
    /// <seealso cref="Slalom.Stacks.Runtime.ExecutionContext" />
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

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalExecutionContext" /> class.
        /// </summary>
        /// <param name="applicationName">The mame of the application.</param>
        /// <param name="environment">The current environment. (Development, Quality Assurance, Production)</param>
        /// <param name="host">The host.</param>
        /// <param name="path">The execution path.</param>
        /// <param name="correlationId">The correlation identifier.</param>
        /// <param name="sessionId">The session identifier.</param>
        /// <param name="user">The current user.</param>
        /// <param name="userHostAddress">The user host address.</param>
        /// <param name="machineName">The name of the machine.</param>
        /// <param name="threadId">The current thread identifier.</param>
        public LocalExecutionContext(string applicationName, string environment, string host, string path, string correlationId, string sessionId, ClaimsPrincipal user, string userHostAddress, string machineName, int threadId)
            : base(applicationName, environment, host, path, correlationId, sessionId, user, userHostAddress, machineName, threadId)
        {
        }
    }
}