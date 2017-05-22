using System;
using System.IO;
using Autofac;
using Newtonsoft.Json;
using Slalom.Stacks.Serialization;
using Slalom.Stacks.Services.OpenApi;
using Slalom.Stacks.Text;

#pragma warning disable 1591

namespace Slalom.Stacks.ConsoleClient
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var collection = new SchemaCollection();
                var result = collection.GetOrAdd(typeof(OpenApiDocument));


                result.OutputToJson();

                //using (var stack = new Stack())
                //{
                //    var result = stack.Send("_system/open-api").Result;

                //    result.OutputToJson();

                //    File.WriteAllText("output.json", JsonConvert.SerializeObject(result.Response, DefaultSerializationSettings.Instance));
                //}
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}