using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Communication
{
    public class CommandExecutionFailedEvent : Event
    {
        public CommandExecutionFailedEvent(ICommand command, ICommandResult result)
        {
            this.ValidationErrors = result.ValidationErrors;
            this.RaisedException = result.RaisedException;
            this.CommandName = command.CommandName;
            this.CommandId = command.Id;
        }

        public Guid CommandId { get; set; }

        public string CommandName { get; set; }

        public Exception RaisedException { get; set; }

        public IEnumerable<ValidationError> ValidationErrors { get; set; }
    }
}
