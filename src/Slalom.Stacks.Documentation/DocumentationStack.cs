using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Services;

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
            foreach (var endPoint in services.Services.SelectMany(e => e.EndPoints).OrderBy(e => e.Path))
            {
                Console.WriteLine($"{endPoint.Name}[{endPoint.EndPointType.Split(',')[0].Split('.').Last()}] - {endPoint.Path}");
                if (endPoint.Summary != null)
                {
                    Console.WriteLine($"  {endPoint.Summary}");
                }
                Console.WriteLine($"  Input[{endPoint.RequestName}]");
                foreach (var property in endPoint.RequestProperties)
                {
                    Console.WriteLine($"    {property.Name}[{property.PropertyType}] - {property.Comments.Value}");
                    if (property.Validation != null)
                    {
                        Console.WriteLine($"      {property.Validation.ValidationName}: {property.Validation.Message}");
                    }
                }
                Console.WriteLine($"  Output[{endPoint.ResponseName}]");
                foreach (var property in endPoint.ResponseProperties)
                {
                    Console.WriteLine($"    {property.Name}[{property.PropertyType}] - {property.Comments.Value}");
                }
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
