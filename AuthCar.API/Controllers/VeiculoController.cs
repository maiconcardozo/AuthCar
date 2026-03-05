using AuthCar.API.Swagger;
using AuthCar.Application.Commands.Veiculo;
using AuthCar.Application.DTOs;
using AuthCar.Application.Queries;
using AuthCar.Shared.Exceptions;
using Foundation.Shared.Validations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

using Microsoft.AspNetCore.Authorization;
/// <summary>
/// Operações relacionadas ao veículo
/// </summary>
[Authorize]
[ApiController]
[Route("AuthCar/[controller]")]
public class VeiculoController : ControllerBase
{
    private readonly IMediator _mediator;

    public VeiculoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Retorna todos os veículos cadastrados
    /// </summary>
    [HttpGet("GetAllVeiculo")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<VeiculoResponseDTO>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
    [SwaggerResponseExample(StatusCodes.Status409Conflict, typeof(ProblemDetailsConflictExample))]
    [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
    public async Task<IActionResult> GetAllVeiculo()
    {
        try
        {
            var result = await _mediator.Send(new ListVeiculosQuery());
            var successResponse = SuccessResponseExampleFactory.ForSuccess(result, "Requisição realizada com sucesso.", HttpContext.Request.Path);
            return Ok(successResponse);
        }
        catch (InvalidOperationException ex)
        {
            var problemDetails = ProblemDetailsExampleFactory.ForBadRequest(ex.Message, HttpContext.Request.Path);
            return BadRequest(problemDetails);
        }
        catch (UnauthorizedAccessException ex)
        {
            var problemDetails = ProblemDetailsExampleFactory.ForUnauthorized(ex.Message, HttpContext.Request.Path);
            return Unauthorized(problemDetails);
        }
        catch (ConflictException ex)
        {
            var problemDetails = ProblemDetailsExampleFactory.ForConflict(ex.Message, HttpContext.Request.Path);
            return Conflict(problemDetails);
        }
        catch (Exception ex)
        {
            var problemDetails = ProblemDetailsExampleFactory.ForInternalServerError(ex.Message, HttpContext.Request.Path);
            return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
        }
    }

    /// <summary>
    /// Retorna um veículo pelo Codigo
    /// </summary>
    [HttpGet("GetVeiculoByCodigo/{codigo}")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(VeiculoResponseDTO))]
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
        try
        {
            var result = await _mediator.Send(new GetVeiculoByCodigoQuery { Codigo = codigo });

            if (result == null)
            {
                var notFoundDetails = ProblemDetailsExampleFactory.ForNotFound("Veículo não encontrado.", HttpContext.Request.Path);
                return NotFound(notFoundDetails);
            }

            var successResponse = SuccessResponseExampleFactory.ForSuccess(result, "Requisição realizada com sucesso.", HttpContext.Request.Path);
            return Ok(successResponse);
        }
        catch (InvalidOperationException ex)
        {
            var problemDetails = ProblemDetailsExampleFactory.ForBadRequest(ex.Message, HttpContext.Request.Path);
            return BadRequest(problemDetails);
        }
        catch (UnauthorizedAccessException ex)
        {
            var problemDetails = ProblemDetailsExampleFactory.ForUnauthorized(ex.Message, HttpContext.Request.Path);
            return Unauthorized(problemDetails);
        }
        catch (ConflictException ex)
        {
            var problemDetails = ProblemDetailsExampleFactory.ForConflict(ex.Message, HttpContext.Request.Path);
            return Conflict(problemDetails);
        }
        catch (Exception ex)
        {
            var problemDetails = ProblemDetailsExampleFactory.ForInternalServerError(ex.Message, HttpContext.Request.Path);
            return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
        }
    }

    /// <summary>
    /// Adiciona um novo veículo
    /// </summary>
    [HttpPost("AddVeiculo")]
    [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(VeiculoResponseDTO))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    [SwaggerResponseExample(StatusCodes.Status201Created, typeof(SucessDetailsExample))]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
    [SwaggerResponseExample(StatusCodes.Status409Conflict, typeof(ProblemDetailsConflictExample))]
    [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
    public async Task<IActionResult> Add([FromBody] VeiculoRequestDTO veiculoDto, [FromServices] IServiceProvider serviceProvider)
    {
        var validationResult = await ValidationHelper.ValidateEntityAsync(veiculoDto, serviceProvider, this);
        if (validationResult != null)
            return validationResult;

        try
        {
            var command = new AddVeiculoCommand
            {
                Descricao = veiculoDto.Descricao,
                Marca = (AuthCar.Domain.Enums.Marca)veiculoDto.Marca,
                Modelo = veiculoDto.Modelo,
                Valor = veiculoDto.Valor
            };

            var usuario = await _mediator.Send(command);
            var successResponse = SuccessResponseExampleFactory.ForSuccess(usuario, "Veículo criado com sucesso.", HttpContext.Request.Path);
            return Ok(successResponse);
        }
        catch (InvalidOperationException ex)
        {
            var problemDetails = ProblemDetailsExampleFactory.ForBadRequest(ex.Message, HttpContext.Request.Path);
            return BadRequest(problemDetails);
        }
        catch (UnauthorizedAccessException ex)
        {
            var problemDetails = ProblemDetailsExampleFactory.ForUnauthorized(ex.Message, HttpContext.Request.Path);
            return Unauthorized(problemDetails);
        }
        catch (ConflictException ex)
        {
            var problemDetails = ProblemDetailsExampleFactory.ForConflict(ex.Message, HttpContext.Request.Path);
            return Conflict(problemDetails);
        }
        catch (Exception ex)
        {
            var problemDetails = ProblemDetailsExampleFactory.ForInternalServerError(ex.Message, HttpContext.Request.Path);
            return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
        }
    }

    /// <summary>
    /// Atualiza um veículo existente
    /// </summary>
    [HttpPut("UpdateVeiculo/{codigo}")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(VeiculoResponseDTO))]
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
    public async Task<IActionResult> Update(Guid codigo, [FromBody] VeiculoRequestDTO veiculoDto, [FromServices] IServiceProvider serviceProvider)
    {
        var validationResult = await ValidationHelper.ValidateEntityAsync(veiculoDto, serviceProvider, this);
        if (validationResult != null)
            return validationResult;

        try
        {
            var command = new UpdateVeiculoCommand
            {
                Codigo = codigo,
                Descricao = veiculoDto.Descricao,
                Marca = (AuthCar.Domain.Enums.Marca)veiculoDto.Marca,
                Modelo = veiculoDto.Modelo,
                Valor = veiculoDto.Valor
            };
            var result = await _mediator.Send(command);
            var successResponse = SuccessResponseExampleFactory.ForSuccess(result, "Veículo atualizado com sucesso.", HttpContext.Request.Path);
            return Ok(successResponse);
        }
        catch (InvalidOperationException ex)
        {
            var problemDetails = ProblemDetailsExampleFactory.ForBadRequest(ex.Message, HttpContext.Request.Path);
            return BadRequest(problemDetails);
        }
        catch (UnauthorizedAccessException ex)
        {
            var problemDetails = ProblemDetailsExampleFactory.ForUnauthorized(ex.Message, HttpContext.Request.Path);
            return Unauthorized(problemDetails);
        }
        catch (ConflictException ex)
        {
            var problemDetails = ProblemDetailsExampleFactory.ForConflict(ex.Message, HttpContext.Request.Path);
            return Conflict(problemDetails);
        }
        catch (Exception ex)
        {
            var problemDetails = ProblemDetailsExampleFactory.ForInternalServerError(ex.Message, HttpContext.Request.Path);
            return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
        }
    }

    /// <summary>
    /// Exclui um veículo
    /// </summary>
    [HttpDelete("DeleteVeiculo/{codigo}")]
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
        try
        {
            var existingVeiculo = await _mediator.Send(new GetVeiculoByCodigoQuery { Codigo = codigo });

            if (existingVeiculo == null)
            {
                var notFoundDetails = ProblemDetailsExampleFactory.ForNotFound("Veículo não encontrado.", HttpContext.Request.Path);
                return NotFound(notFoundDetails);
            }

            await _mediator.Send(new DeleteVeiculoCommand { Codigo = codigo });

            var successResponse = new SucessDetails
            {
                Detail = "Veículo excluído com sucesso.",
                Data = existingVeiculo,
            };

            return Ok(successResponse);
        }
        catch (InvalidOperationException ex)
        {
            var problemDetails = ProblemDetailsExampleFactory.ForBadRequest(ex.Message, HttpContext.Request.Path);
            return BadRequest(problemDetails);
        }
        catch (UnauthorizedAccessException ex)
        {
            var problemDetails = ProblemDetailsExampleFactory.ForUnauthorized(ex.Message, HttpContext.Request.Path);
            return Unauthorized(problemDetails);
        }
        catch (ConflictException ex)
        {
            var problemDetails = ProblemDetailsExampleFactory.ForConflict(ex.Message, HttpContext.Request.Path);
            return Conflict(problemDetails);
        }
        catch (Exception ex)
        {
            var problemDetails = ProblemDetailsExampleFactory.ForInternalServerError(ex.Message, HttpContext.Request.Path);
            return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
        }
    }
}