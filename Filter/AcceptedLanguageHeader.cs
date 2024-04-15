using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace API.Filter
{
    public class AcceptedLanguageHeader : Attribute, IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters.Add(item: new OpenApiParameter()
            {
                Name = "Client",
                In = ParameterLocation.Header,
                Required = true,
                Schema = new OpenApiSchema() { Type = "string" }
            });
        }
    }
}
