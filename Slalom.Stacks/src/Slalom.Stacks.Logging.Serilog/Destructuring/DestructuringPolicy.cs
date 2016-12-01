using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using Newtonsoft.Json;
using Serilog.Core;
using Serilog.Debugging;
using Serilog.Events;
using Slalom.Stacks.Communication;
using Slalom.Stacks.Reflection;
using Slalom.Stacks.Serialization;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Logging.Serilog
{
    internal class DestructuringPolicy : IDestructuringPolicy
    {
        private readonly Dictionary<Type, Func<object, ILogEventPropertyValueFactory, LogEventPropertyValue>> _cache = new Dictionary<Type, Func<object, ILogEventPropertyValueFactory, LogEventPropertyValue>>();
        private readonly object _cacheLock = new object();

        public bool TryDestructure(object value, ILogEventPropertyValueFactory propertyValueFactory, out LogEventPropertyValue result)
        {
            var type = value.GetType();
            //lock (_cacheLock)
            //{
            //    Func<object, ILogEventPropertyValueFactory, LogEventPropertyValue> cached;
            //    if (_cache.TryGetValue(type, out cached))
            //    {
            //        result = cached(value, propertyValueFactory);
            //        return true;
            //    }
            //}

            var properties = type.GetPropertiesRecursive()
                                 .ToList();

            var target = new List<LogEventProperty>();
            foreach (var pi in properties)
            {
                var piValue = pi.GetValue(value);
                target.Add(new LogEventProperty(pi.Name, new ScalarValue(JsonConvert.SerializeObject(piValue))));
            }
            result = new StructureValue(target, type.Name);
            return true;

            //lock (_cacheLock)
            //{
            //    _cache[type] = (o, f) => MakeStructure(o, properties, f, type);
            //}

            //return this.TryDestructure(value, propertyValueFactory, out result);
        }

        private static LogEventPropertyValue MakeStructure(object value, IEnumerable<PropertyInfo> properties, ILogEventPropertyValueFactory propertyValueFactory, Type type)
        {
            var structureProperties = new List<LogEventProperty>();
            foreach (var pi in properties)
            {
                if (pi.GetCustomAttributes<IgnoreAttribute>().Any())
                {
                    continue;
                }

                if (pi.GetCustomAttributes<SecureAttribute>().Any())
                {
                    structureProperties.Add(new LogEventProperty(pi.Name, new ScalarValue(SecureAttribute.DefaultDisplayText)));
                    continue;
                }

                object propValue;
                try
                {
                    propValue = pi.GetValue(value);
                }
                catch (TargetInvocationException ex)
                {
                    SelfLog.WriteLine("The property accessor {0} threw exception {1}", pi, ex);
                    propValue = "The property accessor threw an exception: " + ex.InnerException.GetType().Name;
                }

                LogEventPropertyValue pv;

                if (propValue == null)
                {
                    pv = new ScalarValue(null);
                }
                else
                {
                    if (pi.PropertyType.GetTypeInfo().IsGenericType && (pi.PropertyType.GetGenericTypeDefinition() == typeof(List<>) || pi.PropertyType.GetGenericTypeDefinition() == typeof(IEnumerable<>)))
                    {
                        structureProperties.Add(new LogEventProperty(pi.Name, new ScalarValue(JsonConvert.SerializeObject(propValue))));
                        continue;
                    }
                    else
                    {
                        pv = propertyValueFactory.CreatePropertyValue(propValue, true);
                    }
                }

                structureProperties.Add(new LogEventProperty(pi.Name, pv));
            }

            return new StructureValue(structureProperties, type.Name);
        }
    }
}