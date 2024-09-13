using Microsoft.AspNetCore.Mvc.Controllers;
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

            if (attributes.Any())
            {
                // Extract and convert media types
                var mediaTypes = attributes
                    .SelectMany(attr => attr.AcceptableTypes)
                    .Distinct()
                    .Select(mediaType => (IOpenApiAny)new OpenApiString(mediaType)) // Convert to IOpenApiAny
                    .ToList();

                // Add Accept header parameter to the operation
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "X-Citation-Format",
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
        }
    }
}
