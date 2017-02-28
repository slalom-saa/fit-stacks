using System.Reflection;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Services
{
    /// <summary>
    /// A property that is used in an endpoint.
    /// </summary>
    public class EndPointProperty
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EndPointProperty" /> class.
        /// </summary>
        /// <param name="property">The property.</param>
        public EndPointProperty(PropertyInfo property)
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