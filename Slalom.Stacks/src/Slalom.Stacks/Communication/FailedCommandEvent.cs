using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Communication
{
    public class FailedCommandEvent : Event
    {
        public FailedCommandEvent(ICommandResult result)
        {
            this.ValidationErrors = result.ValidationErrors;
            this.RaisedException = result.RaisedException;
        }

        public Exception RaisedException { get; set; }

        public IEnumerable<ValidationError> ValidationErrors { get; set; }
    }
}
