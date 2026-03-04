using AuthCar.Application.DTOs;
using FluentValidation;

namespace AuthCar.Application.Validators
{
    public class UsuarioValidator : AbstractValidator<UsuarioRequestDTO>
    {
        public UsuarioValidator()
        {
            RuleFor(u => u.Nome)
                .NotEmpty()
                .MinimumLength(3);

            RuleFor(u => u.Login)
                .NotEmpty()
                .MinimumLength(3);

            RuleFor(u => u.Senha)
                .NotEmpty()
                .MinimumLength(6);
        }
    }
}