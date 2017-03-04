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

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the summary.
        /// </summary>
        /// <value>The summary.</value>
        public string Summary { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the validation.
        /// </summary>
        /// <value>The validation.</value>
        public string Validation { get; set; }
    }
}