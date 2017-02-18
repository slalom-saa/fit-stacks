using System.Reflection;
using Autofac.Core;

namespace Slalom.Stacks.Configuration
{
    /// <summary>
    /// Used to select all properties.
    /// </summary>
    /// <seealso cref="Autofac.Core.IPropertySelector" />
    internal class AllProperties : IPropertySelector
    {
        /// <summary>
        /// The shared instance of the selector.
        /// </summary>
        public static readonly IPropertySelector Instance = new AllProperties();

        /// <summary>
        /// Provides filtering to determine if property should be injected
        /// </summary>
        /// <param name="propertyInfo">Property to be injected</param>
        /// <param name="instance">Instance that has the property to be injected</param>
        /// <returns>Whether property should be injected</returns>
        public bool InjectProperty(PropertyInfo propertyInfo, object instance)
        {
            return propertyInfo.CanWrite;
        }
    }
}