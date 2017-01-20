using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slalom.Stacks.Validation
{
    public abstract class ValidationAttribute : Attribute
    {
        public ValidationError ValidationError => new ValidationError(this.Code, this.Message, this.HelpUrl);

        public string Message { get; }

        public string Code { get; set; }

        public string HelpUrl { get; set; }

        public ValidationAttribute(string message)
        {
            this.Message = message;
        }

        public abstract bool IsValid(object value);
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class NotNullOrWhitespaceAttribute : ValidationAttribute
    {
        public NotNullOrWhitespaceAttribute(string message) : base(message)
        {
        }

        public override bool IsValid(object value)
        {
            return !String.IsNullOrWhiteSpace(value as String);
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class NotNullAttribute : ValidationAttribute
    {
        public NotNullAttribute(string message) : base(message)
        {
        }

        public override bool IsValid(object value)
        {
            return value != null;
        }
    }
}
