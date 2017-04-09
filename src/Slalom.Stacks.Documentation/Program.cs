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
                var solution = workspace.OpenSolutionAsync(@"C:\Source\Stacks\Rentals\Slalom.Rentals.sln").Result;

                var types = solution.Projects.Select(e => e.GetCompilationAsync().Result).SelectMany(e => e.GetSymbolsWithName(x => true))
                    .OfType<INamedTypeSymbol>().ToList();
                foreach (var symbol in types.Where(e => e.BaseType?.Name == "EndPoint"))
                {
                    target.AddEndPoint(symbol, types);
                }
                
                //#if core
                //                var path = @"C:\Source\Stacks\Core\test\Slalom.Stacks.ConsoleClient\bin\Debug\netcoreapp1.0\Slalom.Stacks.ConsoleClient.dll";

                //                var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(path);

                //#else
                //                var path = @"C:\Source\Stacks\Core\test\Slalom.Stacks.ConsoleClient\bin\Debug\netcoreapp1.0\Slalom.Stacks.ConsoleClient.dll";

                //                var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(path);
                //#endif

                //var document = DocumentElement.Create(assemblies.ToArray());

                target.OutputToJson();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}