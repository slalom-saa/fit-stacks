using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Slalom.Stacks.Messaging.Registry;
using Slalom.Stacks.Reflection;
using Slalom.Stacks.TestKit;
using Slalom.Stacks.Text;

namespace Slalom.Stacks.Documentation
{
    public class DocumentStack : Stack
    {
        public DocumentStack(params object[] markers) : base(markers)
        {
        }

        public void CreateWordDocument(string path)
        {
            var services = this.GetServices();
            var tests = this.Container.Resolve<DiscoveryService>().Find().Where(e => e.GetAllAttributes<TestSubjectAttribute>().Any()).ToList();
            using (var document = new WordDocument(path))
            {
                foreach (var service in services.Hosts.SelectMany(e => e.Services).OrderBy(e => e.Path))
                {
                    if (service.Path?.StartsWith("_") ?? true)
                    {
                        continue;
                    }

                    foreach (var endPoint in service.EndPoints)
                    {
                        var name = service.Name.ToTitle();
                        if (endPoint.Version > 1)
                        {
                            name += " (version " + endPoint.Version + ")";
                        }
                        document.Append(name, "Heading 2");
                        document.Append("v" + endPoint.Version + "/" + service.Path, "Endpoint Path");

                        if (endPoint.Summary != null)
                        {
                            document.Append(service.EndPoints.First().Summary);
                        }
                        document.Append("Parameters", "Heading 3");

                        document.Append(endPoint.RequestProperties);

                        document.Append("Rules", "Heading 3");

                        if (endPoint.RequestProperties.Any(e => e.Validation != null) || endPoint.Rules.Any())
                        {
                            var table = document.AppendTable(1500, 8500);
                            table.AppendRow("Type", "Summary");
                            foreach (var property in endPoint.RequestProperties)
                            {
                                if (property.Validation != null)
                                {
                                    table.AppendRow("Input", property.Validation);
                                }
                            }
                            foreach (var rule in endPoint.Rules)
                            {
                                table.AppendRow(rule.RuleType.ToString(), rule.Comments?.Summary);
                            }
                        }
                        else
                        {
                            document.Append("None");
                        }

                        document.Append("Tested By", "Heading 3");
                        var t = tests.Where(e => e.GetAllAttributes<TestSubjectAttribute>().First().Type == service.ServiceType);
                        if (t.Any())
                        {
                            var table = document.AppendTable(10000);
                            table.AppendRow("Name");
                            foreach (var test in t)
                            {
                                foreach (var method in test.GetMethods())
                                {
                                    if (method.DeclaringType == test)
                                    {
                                        table.AppendRow(test.Name + "_it_" + method.Name);
                                    }
                                }
                            }
                        }
                        else
                        {
                            document.Append("None");
                        }
                    }
                }
                document.Save();
                document.Open();
            }
        }
    }
}
