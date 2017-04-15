﻿using System;
using System.Linq;
using Slalom.Stacks.Services.Messaging;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Services
{
    /// <summary>
    /// Exception that should be raised when child execution fails in a command chain.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class ChainFailedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChainFailedException" /> class.
        /// </summary>
        /// <param name="request">The current request.</param>
        /// <param name="child">The child execution result.</param>
        public ChainFailedException(Request request, MessageResult child)
            : base($"Failed to complete message {request.Message.Id} because of a failed dependent message {child.RequestId}.", child.RaisedException ?? new ValidationException(child.ValidationErrors.ToArray()))
        {
            this.Request = request;
            this.Child = child;
        }

        /// <summary>
        /// Gets the child execution result.
        /// </summary>
        /// <value>The child execution result.</value>
        public MessageResult Child { get; }

        /// <summary>
        /// Gets the current request.
        /// </summary>
        /// <value>The current request.</value>
        public Request Request { get; }
    }
}