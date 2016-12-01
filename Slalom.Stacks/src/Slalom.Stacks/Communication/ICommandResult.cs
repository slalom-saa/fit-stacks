using System;
using System.Collections.Generic;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Communication
{
    public interface ICommandResult
    {
        string CorrelationId { get; }
        bool IsSuccessful { get; }
        Exception RaisedException { get; }
        IEnumerable<ValidationError> ValidationErrors { get; }
    }
}