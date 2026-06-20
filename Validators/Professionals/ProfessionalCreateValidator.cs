using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SaasClinicas.Api.Data;
using SaasClinicas.Api.Dtos.Professionals;

namespace SaasClinicas.Api.Validators.Professionals;

public class ProfessionalCreateValidator : AbstractValidator<ProfessionalCreateDto>
{
    private readonly ApplicationDbContext _context;
    public ProfessionalCreateValidator(ApplicationDbContext context)
    {
        _context = context;

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("O nome do profissional é obrigatorio")
            .Length(4, 255).WithMessage("O nome do profissional deve ter entre 4 e 255 caracteres");

        RuleFor(p => p.Email)
            .NotEmpty().WithMessage("O email do profissional é obrigatorio.")
            .EmailAddress().WithMessage("Email em um formato invalido")
            .MustAsync(async (email, cancellation) =>
            {
                var exists = await _context.Professionals.AnyAsync(p => p.Email == email);
                return !exists;
            }).WithMessage("Email de profissional ja cadastrado.");

        RuleFor(p => p.SessionPrice)
            .NotEmpty().WithMessage("O preço da sessão nao pode ser vazio.")
            .GreaterThan(0).WithMessage("O valor da sessão não pode ser menor que zero");

        RuleFor(p => p.ClinicId)
            .NotEmpty().WithMessage("O profissional deve ter uma clinica")
            .MustAsync(async (clinicId, cancellation) =>
            {
                var exists = await _context.Clinics.AnyAsync(c => c.Id == clinicId);
                return exists;
            }).WithMessage("A clinica informada não existe");
    }
}