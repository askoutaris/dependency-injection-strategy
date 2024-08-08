using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DependencyInjectionStrategy
{
	public class AddSwaggerHeaders : IOperationFilter
	{
		public void Apply(OpenApiOperation operation, OperationFilterContext context)
		{
			if (operation.Parameters == null)
				operation.Parameters = new List<OpenApiParameter>();

			operation.Parameters.Add(new OpenApiParameter
			{
				Name = Constants.Headers.FormattingStrategy,
				In = ParameterLocation.Header,
				Description = "This is a custom header to select formatting strategy.",
				Required = false, // Set to true if the header is required
				Schema = new OpenApiSchema
				{
					Type = "string",
					Enum = Enum.GetNames(typeof(TextFormattingStrategy))
						.Select(value => new OpenApiString(value))
						.Cast<IOpenApiAny>()
						.ToList()
				}
			});
		}
	}
}
