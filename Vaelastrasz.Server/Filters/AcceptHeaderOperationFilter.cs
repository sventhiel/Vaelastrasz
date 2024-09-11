using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Vaelastrasz.Server.Attributes;

namespace Vaelastrasz.Server.Filters
{
    public class AcceptHeaderOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Retrieve the SwaggerAcceptHeaderAttribute from the method
            var attributes = context.MethodInfo
                .GetCustomAttributes(typeof(SwaggerAcceptHeaderAttribute), false)
                .Cast<SwaggerAcceptHeaderAttribute>()
                .ToList();

            if (attributes.Any())
            {
                // Extract the media types and ensure they are distinct
                var mediaTypes = attributes
                    .SelectMany(attr => attr.AcceptableMediaTypes)
                    .Distinct()
                    .Select(mediaType => (IOpenApiAny) new OpenApiString(mediaType)) // Convert media types to OpenApiString
                    .ToList();

                // Add Accept header parameter to the operation
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "Accept",
                    In = ParameterLocation.Header,
                    Description = "Specifies the media type of the response.",
                    Required = false,
                    Schema = new OpenApiSchema
                    {
                        Type = "string",
                        Enum = mediaTypes  // Ensure Enum property is set with OpenApiString
                    }
                });
            }
        }
    }
}
