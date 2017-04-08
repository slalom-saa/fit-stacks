using System;
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
                var workspace = MSBuildWorkspace.Create();
                var solution = workspace.OpenSolutionAsync(@"C:\Users\George Olson\documents\visual studio 2017\Projects\Slalom.Rentals\Slalom.Rentals.sln").Result;

                foreach (var projecct in solution.ProjectIds)
                {
                    Console.WriteLine(projecct);
                }

                //#if core
                //                var path = @"C:\Source\Stacks\Core\test\Slalom.Stacks.ConsoleClient\bin\Debug\netcoreapp1.0\Slalom.Stacks.ConsoleClient.dll";

                //                var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(path);

                //#else
                //                var path = @"C:\Source\Stacks\Core\test\Slalom.Stacks.ConsoleClient\bin\Debug\netcoreapp1.0\Slalom.Stacks.ConsoleClient.dll";

                //                var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(path);
                //#endif

                //                var document = DocumentElement.Create(assembly);

                //                document.OutputToJson();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}