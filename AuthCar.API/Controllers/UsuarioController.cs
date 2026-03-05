using AuthCar.API.Swagger;
using AuthCar.Application.Commands.Usuario;
using AuthCar.Application.DTOs;
using AuthCar.Application.Queries;
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
    /// Operações relacionadas ao usuário
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("AuthCar/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsuarioController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retorna todos os usuários cadastrados
        /// </summary>
        [HttpGet("GetAllUsuario")]
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
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new ListUsuariosQuery());
            var successResponse = SuccessResponseExampleFactory.ForSuccess(result, "Requisição realizada com sucesso.", HttpContext.Request.Path);
            return Ok(successResponse);
        }

        /// <summary>
        /// Retorna um usuário pelo Código
        /// </summary>
        [HttpGet("GetUsuarioByCodigo/{codigo}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(UsuarioResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProblemDetailsNotFoundExample))]
        [SwaggerResponseExample(StatusCodes.Status409Conflict, typeof(ProblemDetailsConflictExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public async Task<IActionResult> GetByCodigo(Guid codigo)
        {
            var result = await _mediator.Send(new GetUsuarioByCodigoQuery { Codigo = codigo });

            if (result == null)
            {
                var notFoundDetails = ProblemDetailsExampleFactory.ForNotFound("Usuário não encontrado.", HttpContext.Request.Path);
                return NotFound(notFoundDetails);
            }

            var successResponse = SuccessResponseExampleFactory.ForSuccess(result, "Requisição realizada com sucesso.", HttpContext.Request.Path);
            return Ok(successResponse);
        }

        /// <summary>
        /// Adiciona um novo usuário
        /// </summary>
        [HttpPost("AddUsuario")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(UsuarioResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status409Conflict, typeof(ProblemDetailsConflictExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public async Task<IActionResult> Add([FromBody] UsuarioRequestDTO usuarioDto, [FromServices] IServiceProvider serviceProvider)
        {
            var validationResult = await ValidationHelper.ValidateEntityAsync(usuarioDto, serviceProvider, this);
            if (validationResult != null) return validationResult;

            var command = new AddUsuarioCommand
            {
                Nome = usuarioDto.Nome,
                Login = usuarioDto.Login,
                Senha = usuarioDto.Senha
            };

            var usuario = await _mediator.Send(command);
            var successResponse = SuccessResponseExampleFactory.ForSuccess(usuario, "Usuário criado com sucesso.", HttpContext.Request.Path);
            return Ok(successResponse);
        }

        /// <summary>
        /// Atualiza um usuário existente
        /// </summary>
        [HttpPut("UpdateUsuario/{codigo}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(UsuarioResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProblemDetailsNotFoundExample))]
        [SwaggerResponseExample(StatusCodes.Status409Conflict, typeof(ProblemDetailsConflictExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public async Task<IActionResult> Update(Guid codigo, [FromBody] UsuarioRequestDTO usuarioDto, [FromServices] IServiceProvider serviceProvider)
        {
            var validationResult = await ValidationHelper.ValidateEntityAsync(usuarioDto, serviceProvider, this);
            if (validationResult != null) return validationResult;

            var command = new UpdateUsuarioCommand
            {
                Codigo = codigo,
                Nome = usuarioDto.Nome,
                Login = usuarioDto.Login,
                Senha = usuarioDto.Senha
            };

            var result = await _mediator.Send(command);
            var successResponse = SuccessResponseExampleFactory.ForSuccess(result, "Usuário atualizado com sucesso.", HttpContext.Request.Path);
            return Ok(successResponse);

        }

        /// <summary>
        /// Exclui um usuário
        /// </summary>
        [HttpDelete("DeleteUsuario/{codigo}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(SucessDetails))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProblemDetailsNotFoundExample))]
        [SwaggerResponseExample(StatusCodes.Status409Conflict, typeof(ProblemDetailsConflictExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public async Task<IActionResult> Delete(Guid codigo)
        {
            var existingUsuario = await _mediator.Send(new GetUsuarioByCodigoQuery { Codigo = codigo });
            if (existingUsuario == null)
            {
                var notFoundDetails = ProblemDetailsExampleFactory.ForNotFound("Usuário não encontrado.", HttpContext.Request.Path);
                return NotFound(notFoundDetails);
            }

            await _mediator.Send(new DeleteUsuarioCommand { Codigo = codigo });

            var successResponse = new SucessDetails
            {
                Detail = "Usuário excluído com sucesso.",
                Data = existingUsuario,
            };

            return Ok(successResponse);
        }
    }
}