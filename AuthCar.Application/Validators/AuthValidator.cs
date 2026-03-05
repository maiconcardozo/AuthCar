using AuthCar.Application.DTOs.Auth;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthCar.Application.Validators
{
    public class AuthValidator : AbstractValidator<AuthRequestDTO>
    {
        public AuthValidator()
        {
            RuleFor(u => u.Login)
                .NotEmpty().WithMessage("O login é obrigatório.")
                .MinimumLength(3).WithMessage("O login deve ter no mínimo 3 caracteres.");

            RuleFor(u => u.Senha)
                .NotEmpty().WithMessage("A senha é obrigatória.")
                .MinimumLength(6).WithMessage("A senha deve ter no mínimo 6 caracteres.");
        }
    }
}
