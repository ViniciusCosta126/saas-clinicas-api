using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SaasClinicas.Api.Data;
using SaasClinicas.Api.Dtos.Auth;
using SaasClinicas.Api.Dtos.Clinics;

namespace SaasClinicas.Api.Validators.Auth;

public class RegisterValidator : AbstractValidator<RegisterDto>
{
    private readonly ApplicationDbContext _context;

    public RegisterValidator(ApplicationDbContext context)
    {
        _context = context;

        RuleFor(r => r.Clinic)
            .NotNull().WithMessage("Os dados da clínica são obrigatórios")
            .SetValidator(new ClinicRegisterValidator(context));

        RuleFor(r => r.User)
            .NotNull().WithMessage("Os dados do usuário são obrigatórios")
            .SetValidator(new UserRegisterValidator(context));
    }
}

public class ClinicRegisterValidator : AbstractValidator<ClinicCreateDto>
{
    private readonly ApplicationDbContext _context;

    public ClinicRegisterValidator(ApplicationDbContext context)
    {
        _context = context;

        RuleFor(c => c.ClinicName)
            .NotEmpty().WithMessage("O nome da clínica é obrigatório")
            .Length(4, 255).WithMessage("O nome da clínica deve ter entre 4 e 255 caracteres");

        RuleFor(c => c.ResponsibleName)
            .NotEmpty().WithMessage("O nome do responsável é obrigatório")
            .Length(4, 255).WithMessage("O nome do responsável deve ter entre 4 e 255 caracteres");

        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("O email da clínica é obrigatório")
            .EmailAddress().WithMessage("Formato de email inválido")
            .MustAsync(async (email, cancellation) =>
            {
                var exists = await _context.Clinics.AnyAsync(c => c.Email == email);
                return !exists;
            }).WithMessage("Email de clínica já cadastrado");

        RuleFor(c => c.Phone)
            .NotEmpty().WithMessage("O telefone da clínica deve ser preenchido")
            .Matches(@"^\d{10,11}$").WithMessage("O telefone deve ter 10 ou 11 dígitos");
    }
}

public class UserRegisterValidator : AbstractValidator<UserRegisterDto>
{
    private readonly ApplicationDbContext _context;

    public UserRegisterValidator(ApplicationDbContext context)
    {
        _context = context;

        RuleFor(u => u.Name)
            .NotEmpty().WithMessage("O nome do usuário é obrigatório")
            .Length(4, 255).WithMessage("O nome do usuário deve ter entre 4 e 255 caracteres");

        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("O email do usuário é obrigatório")
            .EmailAddress().WithMessage("Formato de email inválido")
            .MustAsync(async (email, cancellation) =>
            {
                var exists = await _context.Users.AnyAsync(u => u.Email == email);
                return !exists;
            }).WithMessage("Email de usuário já cadastrado");

        RuleFor(u => u.Phone)
            .NotEmpty().WithMessage("O telefone do usuário deve ser preenchido")
            .Matches(@"^\d{10,11}$").WithMessage("O telefone deve ter 10 ou 11 dígitos");

        RuleFor(u => u.Cpf)
            .NotEmpty().WithMessage("O CPF do usuário é obrigatório")
            .Matches(@"^\d{11}$").WithMessage("O CPF deve ter 11 dígitos")
            .MustAsync(async (cpf, cancellation) =>
            {
                var exists = await _context.Users.AnyAsync(u => u.Cpf == cpf);
                return !exists;
            }).WithMessage("CPF de usuário já cadastrado");

        RuleFor(u => u.Password)
            .NotEmpty().WithMessage("A senha é obrigatória")
            .Length(6, 255).WithMessage("A senha deve ter entre 6 e 255 caracteres");
    }
}
