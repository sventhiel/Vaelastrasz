using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Vaelastrasz.Server.Attributes;

namespace Vaelastrasz.Server.Filters
{
    public class SwaggerCustomHeaderOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Retrieve the SwaggerAcceptHeaderAttribute from the method
            var attributes = context.MethodInfo
                .GetCustomAttributes(typeof(SwaggerCustomHeaderAttribute), false)
                .Cast<SwaggerCustomHeaderAttribute>()
                .ToList();

            if (attributes.Count == 1)
            {
                var attribute = attributes.Single();

                // Extract and convert media types
                var mediaTypes = attribute.AcceptableTypes
                    .Distinct()
                    .Select(mediaType => (IOpenApiAny)new OpenApiString(mediaType)) // Convert to IOpenApiAny
                    .ToList();

                // Add Accept header parameter to the operation
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = attribute.HeaderName,
                    In = ParameterLocation.Header,
                    Description = "Specifies the media type of the response.",
                    Required = true,
                    Schema = new OpenApiSchema
                    {
                        Type = "string",
                        Enum = mediaTypes // Set Enum property with IOpenApiAny list
                    }
                });
            }

            if (operation.RequestBody?.Content != null && operation.RequestBody.Content.ContainsKey("multipart/form-data"))
            {
                operation.RequestBody.Content["multipart/form-data"].Schema = new OpenApiSchema
                {
                    Type = "object",
                    Properties =
                {
                    ["file"] = new OpenApiSchema
                    {
                        Type = "string",
                        Format = "binary"
                    }
                }
                };
            }
        }
    }
}