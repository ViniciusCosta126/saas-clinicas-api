using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SaasClinicas.Api.Data;
using SaasClinicas.Api.Dtos.Clinics;

namespace SaasClinicas.Api.Validators.Clinics;

public class ClinicUpdateValidator : AbstractValidator<ClinicUpdateDto>
{
    private readonly ApplicationDbContext _context;
    private readonly int _clinicId;

    public ClinicUpdateValidator(ApplicationDbContext context, int clinicId)
    {
        _context = context;
        _clinicId = clinicId;

        RuleFor(c => c.ClinicName)
            .NotEmpty().WithMessage("O nome da clinica é obrigatorio")
            .Length(4, 255).WithMessage("O nome da clinica deve ter entre 4 e 255 caracteres");

        RuleFor(c => c.ResponsibleName)
            .NotEmpty().WithMessage("O nome do responsavel é obrigatorio")
            .Length(4, 255).WithMessage("O nome do responsavel deve ter entre 4 e 255 caracteres");

        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("O email da clinica nao pode ser vazio")
            .EmailAddress().WithMessage("Formato de email inconsistente")
            .MustAsync(async (email, ct) =>
            {
                var exists = await _context.Clinics
                    .Where(c => c.Id != _clinicId)
                    .AnyAsync(c => c.Email == email);
                return !exists;
            }).WithMessage("Email já cadastrado");

        RuleFor(u => u.Phone)
            .NotEmpty().WithMessage("O telefone da clinica deve ser preenchido")
            .Matches(@"^\d{10,11}$").WithMessage("O telefone deve ter 10 ou 11 digitos");
    }
}