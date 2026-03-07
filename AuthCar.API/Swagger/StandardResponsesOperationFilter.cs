using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace AuthCar.API.Swagger
{
    internal sealed class StandardResponsesOperationFilter : IOperationFilter
    {
        private static readonly JsonSerializerOptions SerializerOptions = new() { WriteIndented = false };

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var problemDetailsSchema = context.SchemaGenerator.GenerateSchema(typeof(ProblemDetails), context.SchemaRepository);
            var successSchema = context.SchemaGenerator.GenerateSchema(typeof(SucessDetails), context.SchemaRepository);

            EnsureResponse(operation, "200", successSchema, new SucessDetailsExample().GetExamples());
            EnsureResponse(operation, "400", problemDetailsSchema, new ProblemDetailsBadRequestExample().GetExamples());
            EnsureResponse(operation, "401", problemDetailsSchema, new ProblemDetailsUnauthorizedExample().GetExamples());
            EnsureResponse(operation, "403", problemDetailsSchema, new ProblemDetailsForbiddenExample().GetExamples());
            EnsureResponse(operation, "404", problemDetailsSchema, new ProblemDetailsNotFoundExample().GetExamples());
            EnsureResponse(operation, "409", problemDetailsSchema, new ProblemDetailsConflictExample().GetExamples());
            EnsureResponse(operation, "422", problemDetailsSchema, new ProblemDetailsUnprocessableEntityExample().GetExamples());
            EnsureResponse(operation, "500", problemDetailsSchema, new ProblemDetailsInternalServerErrorExample().GetExamples());
        }

        private static void EnsureResponse(OpenApiOperation operation, string statusCode, IOpenApiSchema schema, object example)
        {
            var exampleJson = JsonSerializer.Serialize(example, SerializerOptions);
            var exampleNode = JsonNode.Parse(exampleJson);

            operation.Responses[statusCode] = new OpenApiResponse
            {
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["application/json"] = new OpenApiMediaType
                    {
                        Schema = schema,
                        Examples = new Dictionary<string, IOpenApiExample>
                        {
                            ["example"] = new OpenApiExample { Value = exampleNode }
                        }
                    }
                }
            };
        }
    }
}
