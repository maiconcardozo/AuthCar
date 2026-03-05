using AuthCar.Application.DTOs.Auth;
using MediatR;

namespace AuthCar.Application.Commands.Auth
{
    public class AuthCommand : IRequest<AuthResponseDTO>
    {
        public string Login { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
    }
}