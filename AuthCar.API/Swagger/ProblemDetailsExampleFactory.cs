using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace AuthCar.API.Swagger
{
    internal static class ProblemDetailsExampleFactory
    {
        private static ProblemDetails Create(int status, string title, string type, string detail, string instance)
        {
            return new ProblemDetails
            {
                Status = status,
                Title = title,
                Type = type,
                Detail = detail,
                Instance = instance
            };
        }

        public static ProblemDetails ForBadRequest(string detail, string instance)
        {
            return Create(StatusCodes.Status400BadRequest, "Requisição inválida", "https://tools.ietf.org/html/rfc7231#section-6.5.1", detail, instance);
        }

        public static ProblemDetails ForUnauthorized(string detail, string instance)
        {
            return Create(StatusCodes.Status401Unauthorized, "Não autorizado", "https://tools.ietf.org/html/rfc7235#section-3.1", detail, instance);
        }

        public static ProblemDetails ForForbidden(string detail, string instance)
        {
            return Create(StatusCodes.Status403Forbidden, "Acesso proibido", "https://tools.ietf.org/html/rfc7231#section-6.5.3", detail, instance);
        }

        public static ProblemDetails ForNotFound(string detail, string instance)
        {
            return Create(StatusCodes.Status404NotFound, "Não encontrado", "https://tools.ietf.org/html/rfc7231#section-6.5.4", detail, instance);
        }

        public static ProblemDetails ForConflict(string detail, string instance)
        {
            return Create(StatusCodes.Status409Conflict, "Conflito", "https://tools.ietf.org/html/rfc7231#section-6.5.8", detail, instance);
        }

        public static ProblemDetails ForUnprocessableEntity(string detail, string instance)
        {
            return Create(StatusCodes.Status422UnprocessableEntity, "Entidade não processável", "https://tools.ietf.org/html/rfc4918#section-11.2", detail, instance);
        }

        public static ProblemDetails ForInternalServerError(string detail, string instance)
        {
            return Create(StatusCodes.Status500InternalServerError, "Erro interno do servidor", "https://tools.ietf.org/html/rfc7231#section-6.6.1", detail, instance);
        }
    }

    internal class ProblemDetailsBadRequestExample : IExamplesProvider<ProblemDetails>
    {
        public ProblemDetails GetExamples() =>
            ProblemDetailsExampleFactory.ForBadRequest("Um ou mais erros de validação ocorreram.", "/exemplo/instancia");
    }

    internal class ProblemDetailsUnauthorizedExample : IExamplesProvider<ProblemDetails>
    {
        public ProblemDetails GetExamples() =>
            ProblemDetailsExampleFactory.ForUnauthorized("Falha na autenticação: credenciais inválidas.", "/exemplo/instancia");
    }

    internal class ProblemDetailsForbiddenExample : IExamplesProvider<ProblemDetails>
    {
        public ProblemDetails GetExamples() =>
            ProblemDetailsExampleFactory.ForForbidden("Acesso proibido.", "/exemplo/instancia");
    }

    internal class ProblemDetailsNotFoundExample : IExamplesProvider<ProblemDetails>
    {
        public ProblemDetails GetExamples() =>
            ProblemDetailsExampleFactory.ForNotFound("O recurso solicitado não foi encontrado.", "/exemplo/instancia");
    }

    internal class ProblemDetailsConflictExample : IExamplesProvider<ProblemDetails>
    {
        public ProblemDetails GetExamples() =>
            ProblemDetailsExampleFactory.ForConflict("A requisição conflita com o estado atual do recurso.", "/exemplo/instancia");
    }

    internal class ProblemDetailsUnprocessableEntityExample : IExamplesProvider<ProblemDetails>
    {
        public ProblemDetails GetExamples() =>
            ProblemDetailsExampleFactory.ForUnprocessableEntity("Regras de negócio não foram atendidas.", "/exemplo/instancia");
    }

    internal class ProblemDetailsInternalServerErrorExample : IExamplesProvider<ProblemDetails>
    {
        public ProblemDetails GetExamples() =>
            ProblemDetailsExampleFactory.ForInternalServerError("Ocorreu um erro inesperado.", "/exemplo/instancia");
    }
}