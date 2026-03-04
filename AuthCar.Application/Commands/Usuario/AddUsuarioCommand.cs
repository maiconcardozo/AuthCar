using AuthCar.Application.DTOs;
using MediatR;

namespace AuthCar.Application.Commands.Usuario
{
    public class AddUsuarioCommand : IRequest<UsuarioResponseDTO>
    {
        public string Nome { get; set; } = string.Empty;
        public string Login { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
    }
}