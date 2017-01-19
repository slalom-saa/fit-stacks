using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using Slalom.Stacks.Messaging;

namespace WebApplication1.Formatting
{
    public class EventFormatter : IOutputFormatter
    {
        private JsonSerializer _serializer;

        public EventFormatter()
        {
            _serializer = new JsonSerializer()
            {
                ContractResolver = new ApiContractResolver()
            };
        }

        public bool CanWriteResult(OutputFormatterCanWriteContext context)
        {
            return typeof(IEvent).IsAssignableFrom(context.ObjectType);
        }

        public async Task WriteAsync(OutputFormatterWriteContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var response = context.HttpContext.Response;
            response.ContentType = "application/json";
            using (var writer = context.WriterFactory(response.Body, Encoding.UTF8))
            {
                _serializer.Serialize(writer, context.Object);

                await writer.FlushAsync();
            }
        }
    }
}