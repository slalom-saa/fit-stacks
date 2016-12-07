using System;
using System.Collections.Generic;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Communication
{
    /// <summary>
    /// Contract for a command result.
    /// </summary>
    public interface ICommandResult
    {
        /// <summary>
        /// Gets or sets the date and time completed.
        /// </summary>
        /// <value>The date and time completed.</value>
        DateTimeOffset? Completed { get; set; }

        /// <summary>
        /// Gets the correlation identifier for the request.
        /// </summary>
        /// <value>The correlation identifier for the request.</value>
        string CorrelationId { get; }

        /// <summary>
        /// Gets the time elapsed.
        /// </summary>
        /// <value>The time elapsed.</value>
        TimeSpan? Elapsed { get; }

        /// <summary>
        /// Gets or sets a value indicating whether the execution was successful.
        /// </summary>
        /// <value><c>true</c> if the execution was successful; otherwise, <c>false</c>.</value>
        bool IsSuccessful { get; }

        /// <summary>
        /// Gets the raised exception if any.
        /// </summary>
        /// <value>The raised exception.</value>
        Exception RaisedException { get; }

        /// <summary>
        /// Gets or sets the date and time started.
        /// </summary>
        /// <value>The date and time started.</value>
        DateTimeOffset Started { get; set; }

        /// <summary>
        /// Gets any validation errors that were raised.
        /// </summary>
        /// <value>The validation errors that were raised.</value>
        IEnumerable<ValidationError> ValidationErrors { get; }
    }
}