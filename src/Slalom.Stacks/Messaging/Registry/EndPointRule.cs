using System;
using Slalom.Stacks.Messaging.Validation;
using Slalom.Stacks.Text;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Messaging.Registry
{
    /// <summary>
    /// Contains information about an endpoint rule.
    /// </summary>
    public class EndPointRule
    {
        public EndPointRule(Type type)
        {
            this.Name = type.Name.ToSentence();
            var baseType = type.BaseType?.GetGenericTypeDefinition();
            if (baseType == typeof(BusinessRule<>))
            {
                this.RuleType = ValidationType.Business;
            }
            if (baseType == typeof(SecurityRule<>))
            {
                this.RuleType = ValidationType.Security;
            }
            if (baseType == typeof(InputRule<>))
            {
                this.RuleType = ValidationType.Input;
            }
            this.Comments = type.GetComments();
        }

        public Comments Comments { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the rule.
        /// </summary>
        /// <value>The type of the rule.</value>
        public ValidationType RuleType { get; set; }
    }
}