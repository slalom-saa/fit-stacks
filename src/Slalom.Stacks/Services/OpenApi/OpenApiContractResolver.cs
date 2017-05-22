/* 
 * Copyright (c) Stacks Contributors
 * 
 * This file is subject to the terms and conditions defined in
 * the LICENSE file, which is part of this source code package.
 */

using System;
using System.Collections;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Slalom.Stacks.Services.OpenApi
{
    /// <summary>
    /// Contract resolver that supports the serialization of an OpenAPI definition document.
    /// </summary>
    public class OpenApiContractResolver : CamelCasePropertyNamesContractResolver
    {
        /// <inheritdoc />
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);
            var isDefaultValueIgnored = ((property.DefaultValueHandling ?? DefaultValueHandling.Ignore) & DefaultValueHandling.Ignore) != 0;
            if (isDefaultValueIgnored && !typeof(string).IsAssignableFrom(property.PropertyType) && typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
            {
                Predicate<object> newShouldSerialize = obj =>
                {
                    var collection = property.ValueProvider.GetValue(obj) as ICollection;
                    return collection == null || collection.Count != 0;
                };

                var oldShouldSerialize = property.ShouldSerialize;
                property.ShouldSerialize = oldShouldSerialize != null ? o => oldShouldSerialize(o) && newShouldSerialize(o) : newShouldSerialize;
            }
            return property;
        }
    }
}