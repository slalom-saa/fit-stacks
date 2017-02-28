using System.Reflection;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Messaging.Registration
{
    /// <summary>
    /// A property that is defined on the input to a endPoint.
    /// </summary>
    public class ServiceProperty
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceProperty" /> class.
        /// </summary>
        /// <param name="property">The property.</param>
        public ServiceProperty(PropertyInfo property)
        {
            this.Name = property.Name;
            this.Type = property.PropertyType.FullName;
            this.Summary = property.GetComments();
            var validation = property.GetCustomAttribute<ValidationAttribute>(true);
            if (validation != null)
            {
                this.Validation = validation.Message;
            }
        }

        public string Name { get; set; }

        public string Summary { get; set; }

        public string Type { get; set; }

        public string Validation { get; set; }
    }
}