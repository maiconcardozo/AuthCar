using AuthCar.Application.DTOs;
using MediatR;

namespace AuthCar.Application.Commands.Usuario
{
    public class UpdateUsuarioCommand : IRequest<UsuarioResponseDTO>
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Login { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
    }
}