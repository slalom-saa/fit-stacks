/* 
 * Copyright (c) Stacks Contributors
 * 
 * This file is subject to the terms and conditions defined in
 * the LICENSE file, which is part of this source code package.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Slalom.Stacks.Configuration;
using Slalom.Stacks.Services.Inventory;
using Slalom.Stacks.Text;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Services.OpenApi
{
    /// <summary>
    /// This is the root document object for the API specification. It combines what previously was the Resource Listing and API Declaration (version 1.2 and earlier) together into one document.
    /// </summary>
    /// <seealso href="http://swagger.io/specification/#swaggerObject"/>
    public class OpenApiDocument
    {
        /// <summary>
        /// Gets or sets the base path on which the API is served, which is relative to the host. If it is not included, the API is served directly under the host. The value MUST start with a leading slash (/). The basePath does not support path templating.
        /// </summary>
        /// <value>
        /// The base path on which the API is served.
        /// </value>
        public string BasePath { get; set; }

        /// <summary>
        /// Gets or sets the object to hold data types produced and consumed by operations.
        /// </summary>
        /// <value>
        /// The object to hold data types produced and consumed by operations.
        /// </value>
        public SchemaCollection Definitions { get; set; } = new SchemaCollection();

        /// <summary>
        /// Gets or sets the additional external documentation.
        /// </summary>
        /// <value>
        /// The external additional external documentation.
        /// </value>
        public IList<ExternalDocs> ExternalDocs { get; set; }

        /// <summary>
        /// Gets or sets the host (name or ip) serving the API. This MUST be the host only and does not include the scheme nor sub-paths. It MAY include a port. If the host is not included, the host serving the documentation is to be used (including the port). The host does not support path templating.
        /// </summary>
        /// <value>
        /// The host (name or ip) serving the API.
        /// </value>
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets the metadata about the API. The metadata can be used by the clients if needed.
        /// </summary>
        /// <value>
        /// The metadata about the API. The metadata can be used by the clients if needed.
        /// </value>
        public Application Info { get; set; }

        /// <summary>
        /// Gets or sets the available paths and operations for the API.
        /// </summary>
        /// <value>
        /// The available paths and operations for the API.
        /// </value>
        public IDictionary<string, PathItem> Paths { get; set; } = new SortedDictionary<string, PathItem>(new PathComparer());

        /// <summary>
        /// Gets or sets the transfer protocol of the API. Values MUST be from the list: "http", "https", "ws", "wss". If the schemes is not included, the default scheme to be used is the one used to access the Swagger definition itself.
        /// </summary>
        /// <value>
        /// The transfer protocol of the API. Values MUST be from the list: "http", "https", "ws", "wss". If the schemes is not included, the default scheme to be used is the one used to access the Swagger definition itself.
        /// </value>
        public string[] Schemes { get; set; } = { "https", "http" };

        /// <summary>
        /// Gets or sets the Swagger Specification version being used. It can be used by the Swagger UI and other clients to interpret the API listing. The value MUST be "2.0".
        /// </summary>
        /// <value>
        /// The Swagger Specification version being used. It can be used by the Swagger UI and other clients to interpret the API listing. The value MUST be "2.0".
        /// </value>
        public string Swagger { get; set; } = "2.0";

        /// <summary>
        /// Gets or sets the list of tags used by the specification with additional metadata. The order of the tags can be used to reflect on their order by the parsing tools. Not all tags that are used by the Operation Object must be declared. The tags that are not declared may be organized randomly or based on the tools' logic. Each tag name in the list MUST be unique.
        /// </summary>
        /// <value>
        /// The list of tags used by the specification with additional metadata.
        /// </value>
        public List<Tag> Tags { get; set; } = new List<Tag> { new Tag { Name = "Stacks", Description = "System defined endpoints." } };

        /// <summary>
        /// Loads the document using hte specified service inventory.
        /// </summary>
        /// <param name="services">The service inventory.</param>
        public void Load(ServiceInventory services)
        {
            this.Info = services.Application;
            var endPoints = services.EndPoints.Where(e => e.Public).ToList();
            foreach (var endPoint in endPoints)
            {
                if (endPoint.RequestType != null)
                {
                    this.Definitions.GetOrAdd(endPoint.RequestType);
                }
                if (endPoint.ResponseType != null)
                {
                    this.Definitions.GetOrAdd(endPoint.ResponseType);
                }

                if (endPoint.Path != null)
                {
                    this.Paths.Add("/" + endPoint.Path, new PathItem
                    {
                        Post = this.GetPostOperation(endPoint),
                        Get = this.GetGetOperation(endPoint)
                    });
                }
            }
        }

        private Operation GetGetOperation(EndPointMetaData endPoint)
        {
            if (endPoint.HttpMethod == "GET")
            {
                var parameters = new List<IParameter>();
                foreach (var property in endPoint.RequestType.GetProperties())
                {
                    var schema = this.Definitions.CreatePrimitiveSchema(property.PropertyType);
                    var required = property.GetCustomAttributes<ValidationAttribute>(true).Any();
                    parameters.Add(new NonBodyParameter
                    {
                        Name = property.Name,
                        Required = required,
                        In = "query",
                        Description = property.GetComments()?.Value,
                        Type = schema.Type,
                        Format = schema.Format
                    });
                }

                return new Operation
                {
                    Tags = this.GetTags(endPoint).ToList(),
                    Summary = endPoint.Name,
                    Description = endPoint.Summary,
                    Consumes = new List<string> { "application/json" },
                    Produces = new List<string> { "application/json" },
                    OperationId = "GET /" + endPoint.Path,
                    Parameters = parameters,
                    Responses = this.GetResponses(endPoint)
                };
            }
            return null;
        }

        private Operation GetPostOperation(EndPointMetaData endPoint)
        {
            if (endPoint.HttpMethod == "POST")
            {
                return new Operation
                {
                    Tags = this.GetTags(endPoint).ToList(),
                    Summary = endPoint.Name,
                    Description = endPoint.Summary,
                    Consumes = new List<string> { "application/json" },
                    Produces = new List<string> { "application/json" },
                    OperationId = "POST /" + endPoint.Path,
                    Parameters = this.GetPostParameters(endPoint).ToList(),
                    Responses = this.GetResponses(endPoint)
                };
            }
            return null;
        }

        private IEnumerable<IParameter> GetPostParameters(EndPointMetaData endPoint)
        {
            if (endPoint.RequestType != null && endPoint.RequestType != typeof(object))
            {
                yield return new BodyParameter
                {
                    Schema = this.Definitions.GetReferenceSchema(endPoint.RequestType, endPoint.RequestType.GetComments()?.Summary)
                };
            }
        }

        private Dictionary<string, Response> GetResponses(EndPointMetaData endPoint)
        {
            var responses = new Dictionary<string, Response>();
            if (endPoint.ResponseType == null)
            {
                responses.Add("204", new Response
                {
                    Description = "No content is returned from this endpoint."
                });
            }
            else
            {
                var responseType = endPoint.ResponseType;
                responses.Add("200", new Response
                {
                    Description = responseType.GetComments()?.Summary ?? "",
                    Schema = this.Definitions.GetReferenceSchema(responseType, endPoint.ResponseType.GetComments()?.Summary)
                });
            }
            var builder = new StringBuilder();
            foreach (var property in endPoint.RequestType.GetProperties())
            {
                foreach (var attribute in property.GetCustomAttributes<ValidationAttribute>(true))
                {
                    builder.AppendLine("1. " + attribute.GetValidationError(property).Message + "\r\n");
                }
            }
            foreach (var source in endPoint.Rules.Where(e => e.RuleType == ValidationType.Input))
            {
                builder.AppendLine(source.Name.ToTitle() + ".  ");
            }
            if (builder.Length > 0)
            {
                responses.Add("400", new Response
                {
                    Schema = this.Definitions.GetReferenceSchema(typeof(ValidationError[]), null),
                    Description = builder.ToString()
                });
            }
            builder.Clear();
            foreach (var source in endPoint.Rules.Where(e => e.RuleType == ValidationType.Business))
            {
                builder.AppendLine("1. " + source.Name.ToTitle() + ".\r\n");
            }
            if (builder.Length > 0)
            {
                responses.Add("409", new Response
                {
                    Schema = this.Definitions.GetReferenceSchema(typeof(ValidationError[]), null),
                    Description = builder.ToString()
                });
            }
            builder.Clear();
            foreach (var source in endPoint.Rules.Where(e => e.RuleType == ValidationType.Security))
            {
                builder.AppendLine(source.Name.ToTitle() + ".    ");
            }
            if (builder.Length > 0)
            {
                responses.Add("403", new Response
                {
                    Schema = this.Definitions.GetReferenceSchema(typeof(ValidationError[]), null),
                    Description = builder.ToString()
                });
            }
            return responses;
        }

        private IEnumerable<string> GetTags(EndPointMetaData endPoint)
        {
            if (endPoint.Path == null)
            {
                yield break;
            }

            if (endPoint.Path.StartsWith("_system"))
            {
                yield return "Stacks";
                yield break;
            }

            var segments = endPoint.Path.Split('/');
            if (segments.Length >= 3)
            {
                yield return segments[1].Replace("-", " ").ToTitle();
            }
        }

        private class PathComparer : IComparer<string>
        {
            public int Compare(string x, string y)
            {
                if (x?.Trim('/').StartsWith("_") == true && y?.Trim('/').StartsWith("_") == false)
                {
                    return 1;
                }
                if (y?.Trim('/').StartsWith("_") == true && x?.Trim('/').StartsWith("_") == false)
                {
                    return -1;
                }
                return string.CompareOrdinal(x, y);
            }
        }
    }
}