using Swashbuckle.AspNetCore.Filters;

namespace AuthCar.API.Swagger
{
    internal static class SuccessResponseExampleFactory
    {
        public static SucessDetails ForSuccess(object? data, string? detail = null, string instance = "/exemplo/instancia")
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
        public SucessDetails GetExamples()
        {
            return SuccessResponseExampleFactory.ForSuccess(
                 new
                 {
                     Id = 9999,
                     Codigo = Guid.NewGuid(),
                     IsActive = true,
                     DtCreated = DateTime.UtcNow,
                     CreatedBy = "UsuárioGenérico",
                     DtUpdated = DateTime.Now,
                     DtDeleted = DateTime.Now,
                     UpdatedBy = "UsuárioGenérico",
                     DeletedBy = "UsuárioGenérico",
                     LstId = new List<int> { 123, 456, 789 },
                     DtCreatedStart = DateTime.Now,
                     DtCreatedEnd = DateTime.Now,
                     AdditionalInfo = "Documentação de objeto genérico",
                 },
                 "A solicitação foi realizada com sucesso.",
                 "/exemplo/instancia");
        }
    }
}
