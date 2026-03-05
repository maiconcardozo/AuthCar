using AuthCar.Application.DTOs;
using FluentValidation;

namespace AuthCar.Application.Validators
{
    public class VeiculoValidator : AbstractValidator<VeiculoRequestDTO>
    {
        public VeiculoValidator()
        {
            RuleFor(v => v.Descricao)
                .NotEmpty().WithMessage("A descrição é obrigatória.")
                .MaximumLength(100).WithMessage("A descrição deve ter no máximo 100 caracteres.");

            RuleFor(v => v.Marca)
                .IsInEnum().WithMessage("A marca é obrigatória e deve ser válida.");

            RuleFor(v => v.Modelo)
                .NotEmpty().WithMessage("O modelo é obrigatório.")
                .MaximumLength(30).WithMessage("O modelo deve ter no máximo 30 caracteres.");
        }
    }
}