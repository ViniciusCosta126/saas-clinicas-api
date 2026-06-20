using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SaasClinicas.Api.Data;
using SaasClinicas.Api.Dtos.Users;

namespace SaasClinicas.Api.Validators.Users;

public class UserUpdateValidator : AbstractValidator<UserUpdateDto>
{
    private readonly ApplicationDbContext _context;
    private readonly int _userId;
    public UserUpdateValidator(ApplicationDbContext context, int userId)
    {
        _context = context;
        _userId = userId;

        RuleFor(u => u.Name)
            .NotEmpty().WithMessage("Nome é obrigatório")
            .Length(4, 255).WithMessage("Nome deve ter entre 4 e 255 caracteres");

        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("Email é obrigatório")
            .EmailAddress().WithMessage("Email inválido")
            .MustAsync(async (email, ct) =>
            {
                var exists = await _context.Users
                    .Where(u => u.Id != _userId)
                    .AnyAsync(u => u.Email == email);
                return !exists;
            }).WithMessage("Email já cadastrado");

        RuleFor(u => u.Phone)
            .NotEmpty().WithMessage("Telefone é obrigatório")
            .Matches(@"^\d{10,11}$").WithMessage("Telefone deve ter 10 ou 11 dígitos");

        RuleFor(u => u.Cpf)
            .NotEmpty().WithMessage("CPF é obrigatório")
            .Matches(@"^\d{11}$").WithMessage("CPF deve ter 11 dígitos")
            .MustAsync(async (cpf, ct) =>
            {
                var exists = await _context.Users
                    .Where(u => u.Id != _userId)
                    .AnyAsync(u => u.Cpf == cpf);
                return !exists;
            }).WithMessage("CPF já cadastrado");

        RuleFor(u => u.Password)
            .NotEmpty().WithMessage("Senha é obrigatória")
            .Length(8, 32).WithMessage("Senha deve ter entre 8 e 32 caracteres");

        RuleFor(u => u.Role)
            .NotEmpty().WithMessage("Role é obrigatório");
    }
}