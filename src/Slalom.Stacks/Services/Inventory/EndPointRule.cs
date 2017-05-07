/* 
 * Copyright (c) Stacks Contributors
 * 
 * This file is subject to the terms and conditions defined in
 * the LICENSE file, which is part of this source code package.
 */

using System;
using System.Reflection;
using Slalom.Stacks.Reflection;
using Slalom.Stacks.Services.Validation;
using Slalom.Stacks.Text;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Services.Inventory
{
    /// <summary>
    /// Contains information about an endpoint rule.
    /// </summary>
    public class EndPointRule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EndPointRule" /> class.
        /// </summary>
        /// <param name="type">The endpoint type.</param>
        public EndPointRule(Type type)
        {
            this.Name = type.Name.ToSentence();
            var baseType = type.GetTypeInfo().BaseType?.GetGenericTypeDefinition();
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

        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <value>The comments.</value>
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