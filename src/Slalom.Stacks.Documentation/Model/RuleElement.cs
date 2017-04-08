using Slalom.Stacks.Messaging.Registry;

namespace Slalom.Stacks.Documentation.Model
{
    public class RuleElement
    {
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
    }
}