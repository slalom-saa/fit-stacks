using System;
using Newtonsoft.Json;
using Slalom.Stacks.Services.Inventory;
using Slalom.Stacks.Services.OpenApi;

namespace Slalom.Stacks.Services.EndPoints
{
    /// <summary>
    /// Gets the OpenAPI definition document.
    /// </summary>
    [EndPoint("_system/open-api", Method = "GET", Name = "Get OpenAPI Definition")]
    public class GetOpenApi : EndPoint
    {
        private readonly ServiceInventory _services;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetOpenApi"/> class.
        /// </summary>
        /// <param name="services">The configured services.</param>
        public GetOpenApi(ServiceInventory services)
        {
            _services = services;
        }

        /// <inheritdoc />
        public override void Receive()
        {
            var document = new OpenApiDocument();
            document.Load(_services);

            this.Respond(JsonConvert.SerializeObject(document, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new OpenApiContractResolver()
            }));
        }
    }
}