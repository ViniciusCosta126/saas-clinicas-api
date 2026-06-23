using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SaasClinicas.Api.Data;
using SaasClinicas.Api.Dtos.Users;

namespace SaasClinicas.Api.Validators.Users;


public class UserCreateValidator : AbstractValidator<UserCreateDto>
{
    private readonly ApplicationDbContext _context;
    public UserCreateValidator(ApplicationDbContext context)
    {
        _context = context;
        RuleFor(u => u.Name)
            .NotEmpty().WithMessage("O nome de usuario não pode ser vazio")
            .Length(3, 255).WithMessage("Nome deve ter entre 4 e 255 caracteres");

        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("O email não pode ser vazio")
            .EmailAddress().WithMessage("Endereco de email invalido")
            .MustAsync(async (email, cancellation) =>
            {
                var exists = await _context.Users.AnyAsync(u => u.Email == email);
                return !exists;
            }).WithMessage("Email de usuario ja cadastrado");

        RuleFor(u => u.Phone)
            .NotEmpty().WithMessage("O telefone do usuario deve ser preenchido")
            .Matches(@"^\d{10,11}$").WithMessage("O telefone deve ter 10 ou 11 digitos");

        RuleFor(u => u.Cpf)
            .NotEmpty().WithMessage("O cpf nao pode ser vazio")
            .Matches(@"^\d{11}$").WithMessage("O cpf deve ter 11 digitos")
            .MustAsync(async (cpf, cancellation) =>
            {
                var exists = await _context.Users.AnyAsync(u => u.Cpf == cpf);
                return !exists;
            }).WithMessage("Cpf de usuario ja cadastrado");

        RuleFor(u => u.ClinicId)
            .NotEmpty().WithMessage("O ID da clinica deve ser preenchido")
            .MustAsync(async (clinicId, cancellation) =>
            {
                var exists = await _context.Clinics.AnyAsync(c => c.Id == clinicId);
                return exists;
            }).WithMessage("A clinica informada não existe");

        RuleFor(u => u.Password)
            .NotEmpty().WithMessage("A senha nao pode ser vazia")
            .Length(8, 32).WithMessage("Senha precisa ter 8 caracteres com no maximo 32 caracteres");

        RuleFor(u => u.Role)
            .IsInEnum().WithMessage("Perfil de acesso invalido ou não reconhecido");
    }
}