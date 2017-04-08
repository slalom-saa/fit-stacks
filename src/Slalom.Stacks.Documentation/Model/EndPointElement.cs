using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Slalom.Stacks.Messaging.Registry;


namespace Slalom.Stacks.Documentation.Model
{
    public class EndPointElement
    {
        public string Name { get; set; }

        public string Path { get; set; }

        public string Timeout { get; set; }

        public int Version { get; set; }

        public List<ParameterElement> Parameters { get; set; } = new List<ParameterElement>();

        public List<RuleElement> Rules { get; set; } = new List<RuleElement>();

        public List<TestElement> Tests { get; set; } = new List<TestElement>();

        public List<DependencyElement> Dependencies { get; set; } = new List<DependencyElement>();

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
