using System;
using System.IO;
using System.Threading.Tasks;
using Autofac;
using Newtonsoft.Json;
using Slalom.Stacks.ConsoleClient.Application.Catalog.Products.Add;
using Slalom.Stacks.ConsoleClient.Domain.Products;
using Slalom.Stacks.Serialization;
using Slalom.Stacks.Services;
using Slalom.Stacks.Services.EndPoints;
using Slalom.Stacks.Services.OpenApi;
using Slalom.Stacks.Text;

#pragma warning disable 1591

namespace Slalom.Stacks.ConsoleClient
{
    [EndPoint("catalog/products/add", Version = 2)]
    public class AddProduct : EndPoint<AddProductCommand>
    {
        /// <inheritdoc />
        public override async Task ReceiveAsync(AddProductCommand command)
        {
            var target = new Product(command.Name, command.Description);

            await this.Domain.Add(target);

            //await this.Send(new SendNotification("adsf", "adf"));

            this.AddRaisedEvent(new ProductAdded(target.Name, target.Description));
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                using (var stack = new Stack())
                {
                  File.WriteAllText("output.json",  stack.Send(new GetOpenApiRequest(null, true)).Result.Response.ToJson());
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}