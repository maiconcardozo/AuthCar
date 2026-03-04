using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AuthCar.API.Swagger
{
    public class LocalizedSwaggerOperationFilter : IOperationFilter
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public LocalizedSwaggerOperationFilter(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            switch (context.MethodInfo.Name)
            {
                // Novas operações para o UsuarioController
                case "GetAll":
                    operation.Summary = "Retorna todos os usuários cadastrados";
                    operation.Description = "Obtém a lista de todos os usuários registrados no sistema.";
                    SetResponseDescription(operation, StatusCodes.Status200OK, "Usuários retornados com sucesso.");
                    SetResponseDescription(operation, StatusCodes.Status400BadRequest, "Requisição inválida.");
                    SetResponseDescription(operation, StatusCodes.Status401Unauthorized, "Não autorizado.");
                    SetResponseDescription(operation, StatusCodes.Status500InternalServerError, "Erro interno do servidor.");
                    break;

                case "GetById":
                    operation.Summary = "Retorna um usuário pelo Id";
                    operation.Description = "Obtém os dados de um usuário específico pelo identificador.";
                    SetResponseDescription(operation, StatusCodes.Status200OK, "Usuário retornado com sucesso.");
                    SetResponseDescription(operation, StatusCodes.Status404NotFound, "Usuário não encontrado.");
                    SetResponseDescription(operation, StatusCodes.Status500InternalServerError, "Erro interno do servidor.");
                    break;

                case "AddUsuario":
                    operation.Summary = "Adiciona um novo usuário";
                    operation.Description = "Cria um novo usuário no sistema.";
                    SetResponseDescription(operation, StatusCodes.Status200OK, "Usuário criado com sucesso.");
                    SetResponseDescription(operation, StatusCodes.Status400BadRequest, "Requisição inválida.");
                    SetResponseDescription(operation, StatusCodes.Status409Conflict, "Conflito ao criar usuário.");
                    SetResponseDescription(operation, StatusCodes.Status500InternalServerError, "Erro interno do servidor.");
                    break;

                case "Update":
                    operation.Summary = "Atualiza um usuário existente";
                    operation.Description = "Atualiza os dados de um usuário já cadastrado.";
                    SetResponseDescription(operation, StatusCodes.Status200OK, "Usuário atualizado com sucesso.");
                    SetResponseDescription(operation, StatusCodes.Status404NotFound, "Usuário não encontrado.");
                    SetResponseDescription(operation, StatusCodes.Status500InternalServerError, "Erro interno do servidor.");
                    break;

                case "Delete":
                    operation.Summary = "Exclui um usuário";
                    operation.Description = "Remove um usuário do sistema.";
                    SetResponseDescription(operation, StatusCodes.Status200OK, "Usuário excluído com sucesso.");
                    SetResponseDescription(operation, StatusCodes.Status404NotFound, "Usuário não encontrado.");
                    SetResponseDescription(operation, StatusCodes.Status500InternalServerError, "Erro interno do servidor.");
                    break;
            }
        }

        private void SetResponseDescription(OpenApiOperation operation, int statusCode, string description)
        {
            var key = statusCode.ToString();
            if (operation.Responses.ContainsKey(key))
            {
                operation.Responses[key].Description = description;
            }
        }
    }
}