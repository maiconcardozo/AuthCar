using MediatR;

namespace AuthCar.Application.Commands.Auth
{
    public class LoginCommand : IRequest<string?>
    {
        public string Login { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
    }
}