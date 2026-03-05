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
            await next(context);

            if (context.Response.StatusCode != StatusCodes.Status200OK)
            {
                ProblemDetails problemDetails = null;


                switch (context.Response.StatusCode)
                {
                    //Tratando apenas o 401 para que caso não seja autorizado, ele sempre vai retornar o padrão do sistema
                    case StatusCodes.Status401Unauthorized:
                        problemDetails = ProblemDetailsExampleFactory.ForUnauthorized("Usuário não Autorizado.", context.Request.Path);
                        break;
                }

                if (!context.Response.HasStarted)
                {
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
                }
            }
        }
    }
}
