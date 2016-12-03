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
        public bool TryDestructure(object value, ILogEventPropertyValueFactory propertyValueFactory, out LogEventPropertyValue result)
        {
            var type = value.GetType();

            var properties = type.GetPropertiesRecursive()
                                 .ToList();

            var target = new List<LogEventProperty>();
            foreach (var pi in properties)
            {
                var piValue = pi.GetValue(value);
                var serializeObject = JsonConvert.SerializeObject(piValue, new DefaultSerializationSettings());
                target.Add(new LogEventProperty(pi.Name, new ScalarValue(serializeObject)));
            }
            result = new StructureValue(target, type.Name);
            return true;
        }
    }
}