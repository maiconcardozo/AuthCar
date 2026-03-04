using AuthCar.Application.DTOs;
using FluentValidation;

namespace AuthCar.Application.Validators
{
    public class VeiculoValidator : AbstractValidator<VeiculoRequestDTO>
    {
        public VeiculoValidator()
        {
            RuleFor(v => v.Descricao)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(v => v.Marca)
                .IsInEnum();

            RuleFor(v => v.Modelo)
                .NotEmpty()
                .MaximumLength(30);
        }
    }
}