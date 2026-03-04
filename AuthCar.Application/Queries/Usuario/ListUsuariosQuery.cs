using AuthCar.Application.DTOs;
using MediatR;

namespace AuthCar.Application.Queries
{
    public class ListUsuariosQuery : IRequest<IEnumerable<UsuarioResponseDTO>>
    {
    }
}