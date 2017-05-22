using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Serialization;
using Slalom.Stacks.Reflection;
using Slalom.Stacks.Text;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Services.OpenApi
{
    public static class Ex
    {
        public static bool IsDictionary(this Type type)
        {
            return type.GetInterfaces().Any(e => e.GetTypeInfo().IsGenericType && e.GetGenericTypeDefinition() == typeof(IDictionary<,>));
        }
    }

    /// <summary>
    /// A collection of OpenAPI schema defintinions.
    /// </summary>
    public class SchemaCollection : SortedDictionary<string, Schema>
    {
        /// <summary>
        /// Gets or adds a shema for the specified type.
        /// </summary>
        /// <param name="type">The type to use for the schema.</param>
        /// <returns>Returns the existing or added schema.</returns>
        public Schema GetOrAdd(Type type, string description = null)
        {
            if (type == null || type == typeof(Object))
            {
                return null;
            }

            if (type.IsNullable())
            {
                type = Nullable.GetUnderlyingType(type);
            }

            if (type.IsPrimitive())
            {
                return this.CreatePrimitiveSchema(type);
            }

            if (!type.IsDictionary() && (typeof(IEnumerable).IsAssignableFrom(type) || type.IsArray))
            {
                return this.CreateArraySchema(type);
            }

            if (this.ContainsKey(type.Name))
            {
                return this[type.Name];
            }

            this.Add(type.Name, null);
            this[type.Name] = this.CreateSchema(type, description);

            //foreach (var item in type.GetProperties())
            //{
            //    this.GetOrAdd(item.PropertyType, item.GetComments()?.Value);
            //}

            return this[type.Name];
        }

        private Schema CreateSchema(Type type, string description = null)
        {
            if (type.IsNullable())
            {
                return this.CreateSchema(Nullable.GetUnderlyingType(type), description);
            }
            if (type.GetTypeInfo().IsEnum)
            {
                return this.CreateEnumSchema(type, description);
            }
            else if (type.IsPrimitive())
            {
                return this.CreatePrimitiveSchema(type, description);
            }
            else if (type.IsDictionary())
            {
                return this.CreateDictionarySchema(type, description);
            }
            else if (type.IsArray || typeof(IEnumerable).IsAssignableFrom(type))
            {
                return this.CreateArraySchema(type);
            }
            else
            {
                return this.CreateObjectSchema(type, description);
            }
        }

        private Schema CreateArraySchema(Type type)
        {
            if (type.IsArray)
            {
                return this.CreateArraySchema(type.GetElementType());
            }
            else if (typeof(IEnumerable).IsAssignableFrom(type) && type != typeof(string))
            {
                if (type.GetTypeInfo().IsInterface)
                {
                    return this.CreateArraySchema(type.GetGenericArguments()[0]);
                }
                else
                {
                    var target = type.GetInterfaces().First(e => e.GetTypeInfo().IsGenericType && e.GetGenericTypeDefinition() == typeof(IEnumerable<>));
                    return this.CreateArraySchema(target.GetGenericArguments()[0]);
                }
            }

            this.GetOrAdd(type);

            return new Schema
            {
                Type = "array",
                Items = type.IsPrimitive() || type == typeof(object) ? this.CreatePrimitiveSchema(type) : this.CreateReferenceSchema(type),
            };
        }

        private Schema CreateObjectSchema(Type type, string description = null)
        {
            var schema = new Schema
            {
                Type = "object",
                Description = description ?? type.GetComments()?.Summary
            };

            var required = new List<string>();
            foreach (var property in type.GetProperties())
            {
                schema.Properties.Add(property.Name, this.GetOrAdd(property.PropertyType));
                if (property.GetCustomAttributes<ValidationAttribute>(true).Any())
                {
                    required.Add(property.Name.ToCamelCase());
                }
            }
            if (required.Any())
            {
                schema.Required = required;
            }
            return schema;
        }

        private Schema CreateDictionarySchema(Type type, string description = null)
        {
            var valueType = type.GetInterfaces().FirstOrDefault(e => e.GetTypeInfo().IsGenericType && e.GetGenericTypeDefinition() == typeof(IDictionary<,>))?.GetGenericArguments().ElementAt(1);
            this.GetOrAdd(valueType);
            if (valueType != null)
            {
                return new Schema
                {
                    Type = "object",
                    //AdditionalProperties = valueType.IsPrimitive()  || valueType == typeof(object) ? this.CreatePrimitiveSchema(valueType) : this.CreateReferenceSchema(valueType, description),
                    Description = description
                };
            }
            return new Schema
            {
                Type = "object",
                Description = description
            };
        }


        internal Schema CreatePrimitiveSchema(Type type, string description = null)
        {
            if (type == typeof(bool))
            {
                return new Schema { Type = "boolean", Description = description };
            }
            if (type == typeof(Guid))
            {
                return new Schema { Type = "string", Format = "uuid", Description = description };
            }
            if (type == typeof(DateTime))
            {
                return new Schema { Type = "string", Format = "date-time", Description = description };
            }
            if (type == typeof(DateTimeOffset))
            {
                return new Schema { Type = "string", Format = "date-time", Description = description };
            }
            if (type == typeof(TimeSpan))
            {
                return new Schema { Type = "string", Example = TimeSpan.FromSeconds(32), Description = description };
            }
            if (type == typeof(char))
            {
                return new Schema { Type = "string", Description = description };
            }
            if (type == typeof(int) || type == typeof(uint) || type == typeof(short) || type == typeof(ushort))
            {
                return new Schema { Type = "integer", Format = "int32", Description = description };
            }
            if (type == typeof(long) || type == typeof(ulong))
            {
                return new Schema { Type = "integer", Format = "int64", Description = description };
            }
            if (type == typeof(float))
            {
                return new Schema { Type = "number", Format = "float", Description = description };
            }
            if (type == typeof(double) || type == typeof(decimal))
            {
                return new Schema { Type = "number", Format = "double", Description = description };
            }
            if (type.IsNullable())
            {
                return this.CreatePrimitiveSchema(Nullable.GetUnderlyingType(type), description);
            }
            return new Schema { Type = "string", Description = description };
        }

        private Schema CreateReferenceSchema(Type type, string description = null)
        {
            return new Schema
            {
                Ref = $"#/definitions/{type.Name.ToCamelCase()}",
                Description = description
            };
        }

        private Schema CreateEnumSchema(Type type, string description = null)
        {
            return new Schema
            {
                Type = "string",
                Enum = Enum.GetNames(type).Select(name => name.ToCamelCase()).ToArray(),
                Description = description ?? type.GetComments()?.Summary
            };
        }

        internal Schema GetReferenceSchema(Type type)
        {
            if (type.IsNullable())
            {
                type = Nullable.GetUnderlyingType(type);
            }

            if (type.IsPrimitive())
            {
                return this.CreatePrimitiveSchema(type);
            }
            else if (type.IsArray)
            {
                return this.CreateArraySchema(type.GetElementType());
            }
            else if (typeof(IEnumerable).IsAssignableFrom(type))
            {
                if (type.GetTypeInfo().IsInterface)
                {
                    return this.CreateArraySchema(type.GetGenericArguments()[0]);
                }
                else
                {
                    var target = type.GetInterfaces().First(e => e.GetTypeInfo().IsGenericType && e.GetGenericTypeDefinition() == typeof(IEnumerable<>));
                    return this.CreateArraySchema(target.GetGenericArguments()[0]);
                }
            }
            else
            {
                return this.CreateReferenceSchema(type);
            }
        }
    }
}
