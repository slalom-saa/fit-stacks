using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using Slalom.Stacks.Documentation.Model;
using Slalom.Stacks.Messaging.Registry;
using Slalom.Stacks.Text;

namespace Slalom.Stacks.Documentation
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            try
            {
                var target = new DocumentElement();

                var workspace = MSBuildWorkspace.Create();
                var solution = workspace.OpenSolutionAsync(args.Length > 0 ? args[0] : @"C:\Source\Stacks\Rentals\Slalom.Rentals.sln").Result;


                var contents = new Dictionary<Project, List<INamedTypeSymbol>>();
                foreach (var project in solution.Projects)
                {
                    var types = project.GetCompilationAsync().Result.GetSymbolsWithName(e => true).OfType<INamedTypeSymbol>().ToList();
                    contents.Add(project, types);
                }

                foreach (var project in contents.Keys)
                {
                    foreach (var type in contents[project])
                    {
                        if (type.BaseType?.Name == "EndPoint")
                        {
                            target.EndPoints.Add(new EndPointElement(type, project, contents.Values.SelectMany(e => e).ToList()));
                        }
                    }
                }

                target.OutputToJson();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}