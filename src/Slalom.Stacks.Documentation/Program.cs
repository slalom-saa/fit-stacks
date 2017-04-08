using System;
using System.Reflection;
using System.Runtime.Loader;
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
#if core
                var path = @"C:\Source\Stacks\Core\test\Slalom.Stacks.ConsoleClient\bin\Debug\netcoreapp1.0\Slalom.Stacks.ConsoleClient.dll";

                var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(path);

#else
                var path = @"C:\Source\Stacks\Core\test\Slalom.Stacks.ConsoleClient\bin\Debug\netcoreapp1.0\Slalom.Stacks.ConsoleClient.dll";

                var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(path);
#endif

                var document = DocumentElement.Create(assembly);

                document.OutputToJson();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}