using FluentValidation;
using SaasClinicas.Api.Dtos.Auth;

namespace SaasClinicas.Api.Validators.Auth;

public class LoginValidator : AbstractValidator<LoginDto>
{
    public LoginValidator()
    {
        RuleFor(l => l.Email)
            .NotEmpty().WithMessage("O email é obrigatório")
            .EmailAddress().WithMessage("Formato de email inválido");

        RuleFor(l => l.Password)
            .NotEmpty().WithMessage("A senha é obrigatória");
    }
}
