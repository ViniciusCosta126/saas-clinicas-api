using Microsoft.AspNetCore.Mvc;
using SaasClinicas.Api.Data;
using SaasClinicas.Api.Dtos.Auth;
using SaasClinicas.Api.Services;
using SaasClinicas.Api.Models;
using Microsoft.EntityFrameworkCore;
using SaasClinicas.Api.Enums;

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
        User? user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);

        if (user == null) return Unauthorized();

        bool validPassword = _passwordHashService.VerifyPassword(dto.Password, user.Password);

        if (!validPassword) return Unauthorized();

        var token = _tokenService.CreateToken(user);

        return Ok(new { token });

    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var existingClinic = await _context.Clinics.FirstOrDefaultAsync(c => c.Email == dto.Clinic.Email);

        if (existingClinic != null) return BadRequest("Email da clinica ja cadastrado.");

        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.User.Email);

        if (existingUser != null) return BadRequest("Email do usuario ja cadastrado");

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
                    Role = (UserRole)dto.User.Role,
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
                return BadRequest($"Erro ao registrar: {e.Message}");
            }
        }
    }
}