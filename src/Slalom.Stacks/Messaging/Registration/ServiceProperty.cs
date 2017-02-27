using System;

namespace Slalom.Stacks.Messaging.Registration
{
    /// <summary>
    /// A property that is defined on the input to a service.
    /// </summary>
    public class ServiceProperty
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceProperty"/> class.
        /// </summary>
        /// <param name="name">The property name.</param>
        /// <param name="type">The property type.</param>
        public ServiceProperty(string name, Type type)
        {
            this.Name = name;
            this.Type = type.FullName;
        }

        public string Comments { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Validation { get; set; }
    }
}