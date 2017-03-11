using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Services;
using Slalom.Stacks.Services.Registry;

namespace Slalom.Stacks.Documentation
{
    public class DocumentStack : Stack
    {
        public DocumentStack(params object[] markers) : base(markers)
        {
        }

        public void WriteToConsole()
        {
            var services = this.GetServices();
            foreach (var endPoint in services.Hosts.SelectMany(e => e.Services).SelectMany(e => e.EndPoints).OrderBy(e => e.Path))
            {
                Console.WriteLine($"{endPoint.EndPointType.Name}] - {endPoint.Path}");
                if (endPoint.Summary != null)
                {
                    Console.WriteLine($"  {endPoint.Summary}");
                }
                Console.WriteLine($"  Input[{endPoint.RequestType}]");
                foreach (var property in endPoint.RequestProperties)
                {
                    Console.WriteLine($"    {property.Name}[{property.Type}] - {property.Summary}");
                    if (property.Validation != null)
                    {
                        Console.WriteLine($"      {property.Validation}: {property.Validation}");
                    }
                }
                Console.WriteLine($"  Output[{endPoint.ResponseType}]");
                //foreach (var property in endPoint)
                //{
                //    Console.WriteLine($"    {property.Name}[{property.PropertyType}] - {property.Comments.Value}");
                //}
                if (endPoint.Rules.Any())
                {
                    Console.WriteLine("  Rules");
                    foreach (var rule in endPoint.Rules)
                    {
                        Console.WriteLine($"    {rule.RuleType}: {rule.Name}");
                    }
                }
            }
        }
    }
}
