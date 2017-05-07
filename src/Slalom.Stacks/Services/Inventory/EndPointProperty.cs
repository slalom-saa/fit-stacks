/* 
 * Copyright (c) Stacks Contributors
 * 
 * This file is subject to the terms and conditions defined in
 * the LICENSE file, which is part of this source code package.
 */

using System.Reflection;
using Slalom.Stacks.Reflection;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Services.Inventory
{
    /// <summary>
    /// A property that is used in an endpoint.
    /// </summary>
    public class EndPointProperty
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EndPointProperty" /> class.
        /// </summary>
        /// <param name="property">The property.</param>
        public EndPointProperty(PropertyInfo property)
        {
            this.Name = property.Name;
            this.Type = property.PropertyType.FullName;
            this.Comments = property.GetComments();
            var validation = property.GetCustomAttribute<ValidationAttribute>(true);
            if (validation != null)
            {
                this.Validation = validation.Message;
            }
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
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the validation.
        /// </summary>
        /// <value>The validation.</value>
        public string Validation { get; set; }
    }
}