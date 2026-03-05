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
                .NotEmpty()
                .MinimumLength(3);

            RuleFor(u => u.Senha)
                .NotEmpty()
                .MinimumLength(6);
        }
    }
}
