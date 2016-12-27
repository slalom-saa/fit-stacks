using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Actors.Imp.Messages
{
    public class CommandValidationFailed
    {
        public IEnumerable<ValidationError> Errors { get; }

        public CommandValidationFailed(IEnumerable<ValidationError> errors)
        {
            this.Errors = errors;
        }
    }
}
