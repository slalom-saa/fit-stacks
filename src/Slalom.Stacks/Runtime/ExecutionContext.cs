using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Newtonsoft.Json;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Serialization;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Runtime
{
    /// <summary>
    /// Represents the execution context and contains information about the request and response. This information is otherwise lost
    /// when processing is multi-threaded or distributed.
    /// </summary>
    public class ExecutionContext
    {
        private readonly List<Event> _raisedEvents = new List<Event>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ExecutionContext"/> class.
        /// </summary>
        protected ExecutionContext()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExecutionContext"/> class.
        /// </summary>
        /// <param name="userName">The current user's username.</param>
        /// <param name="claims">The user's claims.</param>
        protected ExecutionContext(string userName, params Claim[] claims)
        {
            this.User = new ClaimsPrincipal(new ClaimsIdentity(claims.Union(new[] { new Claim(ClaimTypes.Name, userName) })));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExecutionContext" /> class.
        /// </summary>
        /// <param name="applicationName">The mame of the application.</param>
        /// <param name="environment">The current environment. (Development, Quality Assurance, Production)</param>
        /// <param name="path">The execution path.</param>
        /// <param name="correlationId">The correlation identifier.</param>
        /// <param name="sessionId">The session identifier.</param>
        /// <param name="user">The current user.</param>
        /// <param name="sourceAddress">The user host address.</param>
        /// <param name="machineName">The name of the machine.</param>
        /// <param name="threadId">The current thread identifier.</param>
        public ExecutionContext(string applicationName, string environment, string path, string correlationId, string sessionId, ClaimsPrincipal user, string sourceAddress, string machineName, int threadId)
        {
            this.Path = path;
            this.CorrelationId = correlationId;
            this.SessionId = sessionId;
            this.User = user;
            this.SourceAddress = sourceAddress;
            this.MachineName = machineName;
            this.Environment = environment;
            this.ApplicationName = applicationName;
            this.ThreadId = threadId;
        }

        /// <summary>
        /// Gets the name of the application.
        /// </summary>
        /// <value>The name of the application.</value>
        public string ApplicationName { get; }

        /// <summary>
        /// Gets the correlation identifier for the request.
        /// </summary>
        /// <value>The correlation identifier for the request.</value>
        public string CorrelationId { get; }

        /// <summary>
        /// Gets the name of the environment.
        /// </summary>
        /// <value>The name of the environment.</value>
        public string Environment { get; }

        /// <summary>
        /// Gets the name of the machine.
        /// </summary>
        /// <value>The name of the machine.</value>
        public string MachineName { get; }

        /// <summary>
        /// Gets a null execution context.
        /// </summary>
        /// <value>A null execution context.</value>
        public static ExecutionContext Null => new NullExecutionContext();

        /// <summary>
        /// Gets the path.
        /// </summary>
        /// <value>The path.</value>
        public string Path { get; internal set; }

        /// <summary>
        /// Gets the additional events that were raised during execution.
        /// </summary>
        /// <value>The additional events that were raised during execution.</value>
        public IEnumerable<Event> RaisedEvents => _raisedEvents.AsEnumerable();

        /// <summary>
        /// Gets the user's session identifier.
        /// </summary>
        /// <value>The user's session identifier.</value>
        public string SessionId { get; }

        /// <summary>
        /// Gets the calling IP address.
        /// </summary>
        /// <value>The calling IP address.</value>
        public string SourceAddress { get; }

        /// <summary>
        /// Gets or sets the thread identifier.
        /// </summary>
        /// <value>The thread identifier.</value>
        public int ThreadId { get; }

        /// <summary>
        /// Gets the user making the request.
        /// </summary>
        /// <value>The user making the request.</value>
        [JsonConverter(typeof(ClaimsPrincipalConverter))]
        public ClaimsPrincipal User { get; }

        /// <summary>
        /// Adds an additional raised event to the context that will be published on successful completion.
        /// </summary>
        /// <param name="instance">The event to raise.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="instance"/> argument is null.</exception>
        public void AddRaisedEvent(Event instance)
        {
            Argument.NotNull(instance, nameof(instance));

            _raisedEvents.Add(instance);
        }
    }
}