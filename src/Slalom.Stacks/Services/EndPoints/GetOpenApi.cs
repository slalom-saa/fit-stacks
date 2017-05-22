using System;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Slalom.Stacks.Services.Inventory;
using Slalom.Stacks.Services.OpenApi;

namespace Slalom.Stacks.Services.EndPoints
{
    /// <summary>
    /// Gets the [OpenAPI](https://www.openapis.org/) definition for the API.
    /// </summary>
    [EndPoint("_system/endpoints/open-api", Method = "GET", Name = "Get OpenAPI Definition")]
    public class GetOpenApi : EndPoint<GetOpenApiRequest, OpenApiDocument>
    {
        private readonly ServiceInventory _services;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetOpenApi" /> class.
        /// </summary>
        /// <param name="services">The configured services.</param>
        /// <param name="configuration">The configuration.</param>
        public GetOpenApi(ServiceInventory services, IConfiguration configuration)
        {
            _services = services;
            _configuration = configuration;
        }

        /// <inheritdoc />
        public override OpenApiDocument Receive(GetOpenApiRequest instance)
        {
            var document = new OpenApiDocument();
            document.Load(_services);
            document.Host = instance.Host;

            var externalDocs = new ExternalDocs();
            _configuration.GetSection("stacks:externalDocs").Bind(externalDocs);
            if (!String.IsNullOrWhiteSpace(externalDocs.Url))
            {
                document.ExternalDocs = externalDocs;
            }

            return document;
        }
    }
}