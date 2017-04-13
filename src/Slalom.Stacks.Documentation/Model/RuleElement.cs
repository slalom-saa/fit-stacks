using Slalom.Stacks.Messaging;
using Slalom.Stacks.Messaging.Registry;

namespace Slalom.Stacks.Documentation.Model
{
    public class RuleElement
    {
        private string v;
        private Comments comments;

        public string RuleType { get; set; }

        public string Description { get; set; }

        public RuleElement(EndPointProperty property)
        {
            this.RuleType = "Input";
            this.Description = property.Validation;
        }

        public RuleElement(EndPointRule rule)
        {
            this.RuleType = rule.RuleType.ToString();
            this.Description = rule.Comments?.Summary;
        }

        public RuleElement(string type, string description)
        {
            this.RuleType = type;
            this.Description = description;
        }

        public RuleElement(string type, Comments comments)
        {
            this.RuleType = type;
            this.Description = comments.Summary;            
        }
    }
}