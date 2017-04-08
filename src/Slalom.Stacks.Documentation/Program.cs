using System;
using System.Reflection;
using Slalom.Stacks.ConsoleClient.Application.Catalog.Products.Add;
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
                var document = DocumentElement.Create(typeof(AddProduct).GetTypeInfo().Assembly);

                document.OutputToJson();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}