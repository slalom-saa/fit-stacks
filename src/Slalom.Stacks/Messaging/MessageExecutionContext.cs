﻿using System;
using System.Collections.Generic;
using System.Linq;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Services;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Messaging
{
    /// <summary>
    /// The message execution context to track information about the execution.
    /// </summary>
    public class MessageExecutionContext
    {
        private readonly List<Event> _raisedEvents = new List<Event>();
        private readonly List<ValidationError> _validationErrors = new List<ValidationError>();

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageExecutionContext" /> class.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="endPoint">The current endpoint.</param>
        /// <param name="executionContext">The execution.</param>
        /// <param name="parent">The parent.</param>
        public MessageExecutionContext(RequestContext request, Services.EndPoint endPoint, ExecutionContext executionContext, MessageExecutionContext parent = null)
        {
            this.Request = request;
            this.EndPoint = endPoint;
            this.Parent = parent;
            this.ExecutionContext = executionContext;
        }

        /// <summary>
        /// Gets the date and time that execution completed.
        /// </summary>
        /// <value>The date and time that execution completed.</value>
        public DateTimeOffset? Completed { get; private set; }

        /// <summary>
        /// Gets the registry entry.
        /// </summary>
        /// <value>The registry entry.</value>
        public Services.EndPoint EndPoint { get; }

        /// <summary>
        /// Gets the raised exception, if any.
        /// </summary>
        /// <value>The raised exception, if any.</value>
        public Exception Exception { get; private set; }

        /// <summary>
        /// Gets the execution context.
        /// </summary>
        /// <value>The execution context.</value>
        public ExecutionContext ExecutionContext { get; }

        /// <summary>
        /// Gets a value indicating whether the execution was successful.
        /// </summary>
        /// <value><c>true</c> if execution was successful; otherwise, <c>false</c>.</value>
        public bool IsSuccessful => !this.ValidationErrors.Any() && this.Exception == null;

        /// <summary>
        /// Gets the parent context.
        /// </summary>
        /// <value>The parent context.</value>
        public MessageExecutionContext Parent { get; }

        /// <summary>
        /// Gets any raised events.
        /// </summary>
        /// <value>The raised events.</value>
        public IEnumerable<Event> RaisedEvents => _raisedEvents.AsEnumerable();

        /// <summary>
        /// Gets the request context.
        /// </summary>
        /// <value>The request context.</value>
        public RequestContext Request { get; }

        /// <summary>
        /// Gets or sets the response.
        /// </summary>
        /// <value>The response.</value>
        public object Response { get; set; }

        /// <summary>
        /// Gets the date and time that execution started.
        /// </summary>
        /// <value>The date and time that execution started.</value>
        public DateTimeOffset Started { get; } = DateTimeOffset.UtcNow;

        /// <summary>
        /// Gets any validation errors that were raised as part of execution.
        /// </summary>
        /// <value>The validation errors that were raised as part of execution.</value>
        public IEnumerable<ValidationError> ValidationErrors => _validationErrors.AsEnumerable();

        /// <summary>
        /// Adds the raised event.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void AddRaisedEvent(Event instance)
        {
            _raisedEvents.Add(instance);
        }

        /// <summary>
        /// Adds the validation errors.
        /// </summary>
        /// <param name="errors">The erros to add.</param>
        public void AddValidationErrors(IEnumerable<ValidationError> errors)
        {
            _validationErrors.AddRange(errors);
        }

        /// <summary>
        /// Completes the context.
        /// </summary>
        public void Complete()
        {
            this.Completed = DateTimeOffset.UtcNow;
        }

        /// <summary>
        /// Sets the exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public void SetException(Exception exception)
        {
            this.Exception = exception;
        }
    }
}