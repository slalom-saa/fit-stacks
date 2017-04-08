using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Messaging.Registry;
using Slalom.Stacks.Text;


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

    public class ParameterElement
    {
        public ParameterElement(EndPointProperty property)
        {
            this.Name = property.Name;
            this.TypeName = property.Type;
            this.Comments = property.Comments;
        }

        public Comments Comments { get; set; }

        public string TypeName { get; set; }

        public string Name { get; set; }
    }

    public class TestElement
    {
        public TestElement(MethodInfo test)
        {
            this.Name = (test.DeclaringType.Name + "_it_" + test.Name).ToSentence();
        }

        public string Name { get; set; }
    }

    public class EndPointElement
    {
        public string Name { get; set; }

        public string Path { get; set; }

        public string Timeout { get; set; }

        public int Version { get; set; }

        public List<ParameterElement> Parameters { get; set; } = new List<ParameterElement>();

        public List<RuleElement> Rules { get; set; } = new List<RuleElement>();

        public List<TestElement> Tests { get; set; } = new List<TestElement>();

        public EndPointElement(string name, EndPointMetaData endPoint, IEnumerable<Type> tests)
        {
            this.Name = name;
            this.Path = endPoint.Path;
            this.Timeout = endPoint.Timeout.ToString();
            this.Version = endPoint.Version;

            foreach (var property in endPoint.RequestProperties)
            {
                this.Parameters.Add(new ParameterElement(property));
                if (property.Validation != null)
                {
                    this.Rules.Add(new RuleElement(property));
                }
            }

            foreach (var rule in endPoint.Rules)
            {
                this.Rules.Add(new RuleElement(rule));
            }

            foreach (var test in tests)
            {
                foreach (var method in test.GetTypeInfo().GetMethods())
                {
                    if (method.DeclaringType == test)
                    {
                        this.Tests.Add(new TestElement(method));
                    }
                }

            }
        }
    }
}
