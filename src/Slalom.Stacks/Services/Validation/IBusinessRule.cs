﻿/* 
 * Copyright (c) Stacks Contributors
 * 
 * This file is subject to the terms and conditions defined in
 * the LICENSE file, which is part of this source code package.
 */

using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Services.Validation
{
    /// <summary>
    /// Validates an object instance using a business rule.
    /// </summary>
    /// <typeparam name="TValue">The type of message to validate.</typeparam>
    public interface IBusinessRule<in TValue> : IValidate<TValue>
    {
    }
}