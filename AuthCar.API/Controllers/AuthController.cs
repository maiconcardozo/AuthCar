using AuthCar.API.Swagger;
using AuthCar.Application.Commands.Auth;
using AuthCar.Application.DTOs;
using AuthCar.Application.DTOs.Auth;
using AuthCar.Shared.Exceptions;
using Foundation.Shared.Validations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace AuthCar.API.Controllers
{
    /// <summary>
    /// Controller responsável por gerenciar a autenticação dos usuários, incluindo a geração de tokens JWT para acesso aos recursos protegidos da API.
    /// </summary>
    [ApiController]
    [Route("AuthCar/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator mediator;

        public AuthController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Autentica um usuário e gera um token JWT para acesso aos recursos protegidos da API.
        /// </summary>
        [AllowAnonymous]
        [HttpPost("GerarToken")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<UsuarioResponseDTO>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status409Conflict, typeof(ProblemDetailsConflictExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public async Task<IActionResult> GerarToken([FromBody] AuthRequestDTO authRequestDTO, [FromServices] IServiceProvider serviceProvider)
        {
            var validationResult = await ValidationHelper.ValidateEntityAsync(authRequestDTO, serviceProvider, this);
            if (validationResult != null)
            {
                return validationResult;
            }

            var command = new LoginCommand
            {
                Login = authRequestDTO.Login,
                Senha = authRequestDTO.Senha
            };

            var tokenDTO = await mediator.Send(command);
            var successResponse = SuccessResponseExampleFactory.ForSuccess(tokenDTO, "Token gerado com sucesso.", HttpContext.Request.Path);
            return Ok(successResponse);

        }
    }
}
