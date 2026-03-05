using AuthCar.Application.DTOs;
using FluentValidation;

namespace AuthCar.Application.Validators
{
    public class UsuarioValidator : AbstractValidator<UsuarioRequestDTO>
    {
        public UsuarioValidator()
        {
            RuleFor(u => u.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .MinimumLength(3).WithMessage("O nome deve ter no mínimo 3 caracteres.");

            RuleFor(u => u.Login)
                .NotEmpty().WithMessage("O login é obrigatório.")
                .MinimumLength(3).WithMessage("O login deve ter no mínimo 3 caracteres.");

            RuleFor(u => u.Senha)
                .NotEmpty().WithMessage("A senha é obrigatória.")
                .MinimumLength(6).WithMessage("A senha deve ter no mínimo 6 caracteres.");
        }
    }
}