using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SaasClinicas.Api.Data;
using SaasClinicas.Api.Dtos.Professionals;

namespace SaasClinicas.Api.Validators.Professionals;

public class ProfessionalUpdateValidator : AbstractValidator<ProfessionalUpdateDto>
{
    private readonly ApplicationDbContext _context;
    private readonly int _professionalId;

    public ProfessionalUpdateValidator(ApplicationDbContext context, int professionalId)
    {
        _context = context;
        _professionalId = professionalId;

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("O nome do profissional é obrigatorio")
            .Length(4, 255).WithMessage("O nome do profissional deve ter entre 4 e 255 caracteres");

        RuleFor(p => p.Email)
            .NotEmpty().WithMessage("O email do profissional é obrigatorio.")
            .EmailAddress().WithMessage("Email em um formato invalido")
            .MustAsync(async (email, ct) =>
            {
                var exists = await _context.Professionals
                    .Where(u => u.Id != _professionalId)
                    .AnyAsync(u => u.Email == email);
                return !exists;
            }).WithMessage("Email já cadastrado");

        RuleFor(p => p.SessionPrice)
            .NotEmpty().WithMessage("O preço da sessão nao pode ser vazio.")
            .GreaterThan(0).WithMessage("O valor da sessão não pode ser menor que zero");

    }
}