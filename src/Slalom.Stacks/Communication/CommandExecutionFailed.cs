using System;
using System.Collections.Generic;
using System.Linq;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Communication
{
    /// <summary>
    /// Represents an event that is raised when command execution fails.
    /// </summary>
    /// <seealso cref="Slalom.Stacks.Communication.Event" />
    public class CommandExecutionFailed : Event
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandExecutionFailed"/> class.
        /// </summary>
        /// <param name="command">The command that failed.</param>
        /// <param name="result">The result of the execution.</param>
        public CommandExecutionFailed(ICommand command, ICommandResult result)
        {
            this.ValidationErrors = result.ValidationErrors;
            this.RaisedException = result.RaisedException;
            this.CommandName = command.CommandName;
            this.CommandId = command.Id;
        }

        /// <summary>
        /// Gets the command identifier.
        /// </summary>
        /// <value>The command identifier.</value>
        public string CommandId { get; private set; }

        /// <summary>
        /// Gets the name of the command.
        /// </summary>
        /// <value>The name of the command.</value>
        public string CommandName { get; private set; }

        /// <summary>
        /// Gets the raised exception.
        /// </summary>
        /// <value>The raised exception.</value>
        public Exception RaisedException { get; private set; }

        /// <summary>
        /// Gets the validation errors.
        /// </summary>
        /// <value>The validation errors.</value>
        public IEnumerable<ValidationError> ValidationErrors { get; private set; }
    }
}