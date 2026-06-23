using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SaasClinicas.Api.Data;
using SaasClinicas.Api.Dtos.Patients;

namespace SaasClinicas.Api.Validators.Patients;

public class PatientUpdateValidator : AbstractValidator<PatientUpdateDto>
{
    private readonly ApplicationDbContext _context;
    private readonly int _patientId;

    public PatientUpdateValidator(ApplicationDbContext context, int patientId)
    {
        _context = context;
        _patientId = patientId;

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("O nome do paciente é obrigatório")
            .Length(4, 255).WithMessage("O nome do paciente deve ter entre 4 e 255 caracteres");

        RuleFor(p => p.Email)
            .NotEmpty().WithMessage("O email do paciente é obrigatório")
            .EmailAddress().WithMessage("Formato de email inválido")
            .MustAsync(async (email, ct) =>
            {
                var exists = await _context.Patients
                    .Where(p => p.Id != _patientId)
                    .AnyAsync(p => p.Email == email);
                return !exists;
            }).WithMessage("Email já cadastrado");

        RuleFor(p => p.Phone)
            .NotEmpty().WithMessage("O telefone do paciente deve ser preenchido")
            .Matches(@"^\d{10,11}$").WithMessage("O telefone deve ter 10 ou 11 dígitos");

        RuleFor(p => p.Birthday)
            .NotEmpty().WithMessage("A data de nascimento é obrigatória");

        RuleFor(p => p.Cpf)
            .NotEmpty().WithMessage("O CPF do paciente é obrigatório")
            .Matches(@"^\d{11}$").WithMessage("O CPF deve ter 11 dígitos")
            .MustAsync(async (cpf, ct) =>
            {
                var exists = await _context.Patients
                    .Where(p => p.Id != _patientId)
                    .AnyAsync(p => p.Cpf == cpf);
                return !exists;
            }).WithMessage("CPF já cadastrado");
    }
}
