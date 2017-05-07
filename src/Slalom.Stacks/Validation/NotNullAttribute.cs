/* 
 * Copyright (c) Stacks Contributors
 * 
 * This file is subject to the terms and conditions defined in
 * the LICENSE file, which is part of this source code package.
 */

using System;

namespace Slalom.Stacks.Validation
{
    /// <summary>
    /// Validates that a property is not null.
    /// </summary>
    /// <seealso cref="Slalom.Stacks.Validation.ValidationAttribute" />
    [AttributeUsage(AttributeTargets.Property)]
    public class NotNullAttribute : ValidationAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotNullAttribute" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public NotNullAttribute(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Returns true if the object value is valid.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the specified value is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid(object value)
        {
            return value != null;
        }
    }
}