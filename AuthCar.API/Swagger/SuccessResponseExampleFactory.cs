using Swashbuckle.AspNetCore.Filters;

namespace AuthCar.API.Swagger
{
    internal static class SuccessResponseExampleFactory
    {
        public static SucessDetails ForSuccess(object? data, string? detail = null, string instance = "/example/instance")
        {
            return new SucessDetails
            {
                Status = StatusCodes.Status200OK,
                Title = "Ok",
                Detail = detail,
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.3.1",
                Data = data,
                Instance = instance,
            };
        }
    }

    internal class SucessDetailsExample : IExamplesProvider<SucessDetails>
    {
        public SucessDetails GetExamples() =>
            SuccessResponseExampleFactory.ForSuccess(
                new { UserId = 123, UserName = "example.example", Email = "example.example@example.com" },
                "Requisicão foi feita com sucesso!",
                "/example/instance");
    }
}
