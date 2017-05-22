using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Slalom.Stacks.Reflection;
using Slalom.Stacks.Text;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Services.OpenApi
{
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
        public Schema GetOrAdd(Type type)
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

            if (typeof(IEnumerable).IsAssignableFrom(type) || type.IsArray)
            {
                return null;
            }

            if (this.ContainsKey(type.Name))
            {
                return this[type.Name];
            }
           
            this.Add(type.Name, this.CreateSchema(type));

            foreach (var item in type.GetProperties())
            {
                this.GetOrAdd(item.PropertyType);
            }

            return this[type.Name];
        }

        private Schema CreateSchema(Type type, string description = null)
        {
            if (type.GetTypeInfo().IsEnum)
            {
                return this.CreateEnumSchema(type, description);
            }
            else if (type.IsPrimitive())
            {
                return this.CreatePrimitiveSchema(type, description);
            }
            else if (type.IsArray)
            {
                return this.GetOrAdd(type.GetElementType());
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
                return this.CreateObjectSchema(type, description);
            }
        }

        private Schema CreateArraySchema(Type type)
        {
            this.GetOrAdd(type);

            return new Schema
            {
                Type = "array",
                Items = this.CreateReferenceSchema(type)
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
                schema.Properties.Add(property.Name, this.CreateSchema(property.PropertyType, property.GetComments()?.Value));
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

        private Schema CreateReferenceSchema(Type type)
        {
            return new Schema
            {
                Ref = $"#/definitions/{type.Name.ToCamelCase()}"
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
                return this.GetOrAdd(type.GetElementType());
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
