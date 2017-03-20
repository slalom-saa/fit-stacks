using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Services;
using Slalom.Stacks.Services.Registry;
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
                    }
                }
                document.Save();
                document.Open();
            }
        }
    }
}
