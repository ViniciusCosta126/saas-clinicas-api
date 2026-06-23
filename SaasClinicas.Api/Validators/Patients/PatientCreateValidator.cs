using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SaasClinicas.Api.Data;
using SaasClinicas.Api.Dtos.Patients;

namespace SaasClinicas.Api.Validators.Patients;

public class PatientCreateValidator : AbstractValidator<PatientCreateDto>
{
    private readonly ApplicationDbContext _context;

    public PatientCreateValidator(ApplicationDbContext context)
    {
        _context = context;

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("O nome do paciente é obrigatório")
            .Length(3, 255).WithMessage("O nome do paciente deve ter entre 4 e 255 caracteres");

        RuleFor(p => p.Email)
            .NotEmpty().WithMessage("O email do paciente é obrigatório")
            .EmailAddress().WithMessage("Formato de email inválido")
            .MustAsync(async (email, cancellation) =>
            {
                var exists = await _context.Patients.AnyAsync(p => p.Email == email);
                return !exists;
            }).WithMessage("Email de paciente já cadastrado");

        RuleFor(p => p.Phone)
            .NotEmpty().WithMessage("O telefone do paciente deve ser preenchido")
            .Matches(@"^\d{10,11}$").WithMessage("O telefone deve ter 10 ou 11 dígitos");

        RuleFor(p => p.Birthday)
            .NotEmpty().WithMessage("A data de nascimento é obrigatória");

        RuleFor(p => p.Cpf)
            .NotEmpty().WithMessage("O CPF do paciente é obrigatório")
            .Matches(@"^\d{11}$").WithMessage("O CPF deve ter 11 dígitos")
            .MustAsync(async (cpf, cancellation) =>
            {
                var exists = await _context.Patients.AnyAsync(p => p.Cpf == cpf);
                return !exists;
            }).WithMessage("CPF de paciente já cadastrado");

        RuleFor(p => p.ClinicId)
            .NotEmpty().WithMessage("O paciente deve ter uma clínica")
            .MustAsync(async (clinicId, cancellation) =>
            {
                var exists = await _context.Clinics.AnyAsync(c => c.Id == clinicId);
                return exists;
            }).WithMessage("A clínica informada não existe");
    }
}
