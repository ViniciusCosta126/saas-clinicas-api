using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SaasClinicas.Api.Data;
using SaasClinicas.Api.Dtos.Auth;
using SaasClinicas.Api.Services;
using SaasClinicas.Api.Models;
using Microsoft.EntityFrameworkCore;
using SaasClinicas.Api.Validators.Auth;

namespace SaasClinicas.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IPasswordHashService _passwordHashService;

    private readonly ITokenService _tokenService;

    public AuthController(ApplicationDbContext context, IPasswordHashService passwordHashService, ITokenService tokenService)
    {
        _context = context;
        _passwordHashService = passwordHashService;
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var validator = new LoginValidator();
        var result = await validator.ValidateAsync(dto);
        if (!result.IsValid) throw new ValidationException(result.Errors);

        User? user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);

        if (user == null) throw new UnauthorizedAccessException("Email de usuario invalido.");

        bool validPassword = _passwordHashService.VerifyPassword(dto.Password, user.Password);

        if (!validPassword) throw new UnauthorizedAccessException("Senha do usuario incorreta");

        var token = _tokenService.CreateToken(user);

        return Ok(new { token });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var validator = new RegisterValidator(_context);
        var result = await validator.ValidateAsync(dto);
        if (!result.IsValid) throw new ValidationException(result.Errors);

        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                var clinic = new Clinic
                {
                    ClinicName = dto.Clinic.ClinicName,
                    ResponsibleName = dto.Clinic.ResponsibleName,
                    Email = dto.Clinic.Email,
                    Phone = dto.Clinic.Phone
                };
                _context.Clinics.Add(clinic);
                await _context.SaveChangesAsync();

                var user = new User
                {
                    Name = dto.User.Name,
                    Email = dto.User.Email,
                    Phone = dto.User.Phone,
                    Cpf = dto.User.Cpf,
                    Password = _passwordHashService.HashPassword(dto.User.Password),
                    Role = 0,
                    ClinicId = clinic.Id
                };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                var token = _tokenService.CreateToken(user);
                return Ok(new { token, clinicId = clinic.Id, userId = user.Id });

            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                throw new BadHttpRequestException($"Erro ao registrar: {e.Message}");
            }
        }
    }
}