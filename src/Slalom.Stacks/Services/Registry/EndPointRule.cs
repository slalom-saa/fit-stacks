using System;
using System.Linq;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Services.Registry
{
    /// <summary>
    /// Contains information about an endpoint rule.
    /// </summary>
    public class EndPointRule
    {
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