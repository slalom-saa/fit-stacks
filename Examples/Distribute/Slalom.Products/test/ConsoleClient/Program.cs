using Slalom.Products.Application.Catalog.Products.Add;
using Slalom.Stacks;
using Slalom.Stacks.Documentation;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Text;
using Slalom.Stacks.Web.AspNetCore;

namespace ConsoleClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var stack = new Stack(typeof(AddProduct), typeof(Program)))
            {
                stack.RunWebHost();
            }
        }
    }
}