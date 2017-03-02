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
            this.PropertyType = property.PropertyType.FullName;
            this.Comments = property.GetComments();
            var validation = property.GetCustomAttribute<ValidationAttribute>(true);
            if (validation != null)
            {
                this.Validation = new PropertyValidation(validation);
            }
        }

        public string Name { get; set; }

        public Comments Comments { get; set; }

        public string PropertyType { get; set; }

        public PropertyValidation Validation { get; set; }
    }

    public class PropertyValidation
    {
        public PropertyValidation(ValidationAttribute attribute)
        {
            this.Message = attribute.Message;
            this.ValidationType = attribute.GetType().AssemblyQualifiedName;
            this.ValidationName = attribute.GetType().Name.Replace("Attribute", "");
        }

        public string ValidationType { get; set; }

        public string Message { get; set; }

        public string ValidationName { get; set; }
    }
}