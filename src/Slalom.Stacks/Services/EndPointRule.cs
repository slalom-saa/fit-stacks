using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Services
{
    public class EndPointRule
    {
        public ValidationType RuleType { get; set; }

        public string Name { get; set; }
    }
}
