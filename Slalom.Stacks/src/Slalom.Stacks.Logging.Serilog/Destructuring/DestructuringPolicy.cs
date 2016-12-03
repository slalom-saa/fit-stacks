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
using Serilog.Parsing;
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
            foreach (var item in properties)
            {
                if ((item.Name == "Value" && value is ICommand) || item.GetCustomAttributes<IgnoreAttribute>().Any())
                {
                    continue;
                }

                if (item.GetCustomAttributes<SecureAttribute>().Any())
                {
                    target.Add(new LogEventProperty(item.Name, new ScalarValue(SecureAttribute.DefaultDisplayText)));
                    continue;
                }

                var piValue = item.GetValue(value);
                if (piValue == null)
                {
                    target.Add(new LogEventProperty(item.Name, new ScalarValue(null)));
                    continue;
                }

                // TODO: Continue to build out for specific types.
                if (piValue is ClaimsPrincipal)
                {
                    target.Add(new LogEventProperty(item.Name, propertyValueFactory.CreatePropertyValue(new ClaimsPrincipalConverter.ClaimsPrincipalHolder((ClaimsPrincipal)piValue), true)));
                    continue;
                }


                target.Add(new LogEventProperty(item.Name, propertyValueFactory.CreatePropertyValue(piValue, true)));
            }
            result = new StructureValue(target, type.Name);
            return true;
        }
    }
}
