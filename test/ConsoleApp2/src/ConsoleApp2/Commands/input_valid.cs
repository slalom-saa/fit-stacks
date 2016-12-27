using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Communication.Validation;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks
{
    public class input_valid : InputValidationRule<AddProcedureCommand>
    {
        protected override IEnumerable<ValidationError> Validate(AddProcedureCommand instance)
        {
            yield break;
        }
    }
}