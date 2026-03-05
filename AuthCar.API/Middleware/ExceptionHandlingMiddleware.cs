using AuthCar.API.Swagger;
using AuthCar.Shared.Exceptions;
using Foundation.Shared.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AuthCar.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionHandlingMiddleware> logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (InvalidOperationException ex)
            {
                logger.LogError(ex, "Exceção de Requisição Inválida");
                await WriteProblemDetailsAsync(
                    context,
                    ProblemDetailsExampleFactory.ForBadRequest(
                        $"Requisição inválida: {ex.Message}",
                        context.Request.Path
                    ),
                    StatusCodes.Status400BadRequest
                );
            }
            catch (UnauthorizedAccessException ex)
            {
                logger.LogError(ex, "Exceção de Não Autorizado");
                await WriteProblemDetailsAsync(
                    context,
                    ProblemDetailsExampleFactory.ForUnauthorized(
                        $"Acesso não autorizado: {ex.Message}",
                        context.Request.Path
                    ),
                    StatusCodes.Status401Unauthorized
                );
            }
            catch (ConflictException ex)
            {
                logger.LogError(ex, "Exceção de Conflito");
                await WriteProblemDetailsAsync(
                    context,
                    ProblemDetailsExampleFactory.ForConflict(
                        $"Conflito: {ex.Message}",
                        context.Request.Path
                    ),
                    StatusCodes.Status409Conflict
                );
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exceção não tratada");
                await WriteProblemDetailsAsync(
                    context,
                    ProblemDetailsExampleFactory.ForInternalServerError(
                        $"Erro interno do servidor: {ex.Message}",
                        context.Request.Path
                    ),
                    StatusCodes.Status500InternalServerError
                );
            }
        }

        private async Task WriteProblemDetailsAsync(HttpContext context, ProblemDetails problemDetails, int statusCode)
        {
            if (!context.Response.HasStarted)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = statusCode;
                await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
            }
        }
    }
}
