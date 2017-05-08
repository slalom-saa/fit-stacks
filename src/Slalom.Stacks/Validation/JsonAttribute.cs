/* 
 * Copyright (c) Stacks Contributors
 * 
 * This file is subject to the terms and conditions defined in
 * the LICENSE file, which is part of this source code package.
 */

using System;
using Newtonsoft.Json.Linq;

namespace Slalom.Stacks.Validation
{
    /// <summary>
    /// Validates that a property is valid JSON.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class JsonAttribute : ValidationAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonAttribute" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public JsonAttribute(string message) : base(message)
        {
        }

        /// <inheritdoc />
        public override bool IsValid(object value)
        {
            if (value is string)
            {
                var strInput = ((string) value).Trim();
                if (strInput.StartsWith("{") && strInput.EndsWith("}") || strInput.StartsWith("[") && strInput.EndsWith("]"))
                {
                    try
                    {
                        JToken.Parse(strInput);
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
            return false;
        }
    }
}