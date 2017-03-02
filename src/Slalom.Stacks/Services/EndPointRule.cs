using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Messaging.Validation;
using Slalom.Stacks.Reflection;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Services
{
    public class EndPointRule
    {
        public EndPointRule(Type type)
        {
            this.Name = type.Name.Replace("_", " ");
            var bases = type.GetBaseTypes();
            if (bases.Any(e => e.IsGenericType && e.GetGenericTypeDefinition() == typeof(BusinessRule<>)))
            {
                this.RuleType = ValidationType.Business;
            }
            else if (bases.Any(e => e.IsGenericType && e.GetGenericTypeDefinition() == typeof(InputRule<>)))
            {
                this.RuleType = ValidationType.Input;
            }
            else if (bases.Any(e => e.IsGenericType && e.GetGenericTypeDefinition() == typeof(SecurityRule<>)))
            {
                this.RuleType = ValidationType.Business;
            }
        }

        public ValidationType RuleType { get; set; }

        public string Name { get; set; }
    }
}
