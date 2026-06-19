using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaasClinicas.Api.Data;
using SaasClinicas.Api.Dtos.Users;
using SaasClinicas.Api.Models;
using SaasClinicas.Api.Services;

namespace SaasClinicas.Api.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{

    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IPasswordHashService _passwordHashService;
    public UsersController(ApplicationDbContext context, IMapper mapper, IPasswordHashService passwordHashService)
    {
        _context = context;
        _mapper = mapper;
        _passwordHashService = passwordHashService;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<UserResponseDto>>> Get()
    {
        List<User> users = await _context.Users.Where(c => c.DeletedAt == null).ToListAsync();

        var response = _mapper.Map<List<UserResponseDto>>(users);

        return Ok(response);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<UserResponseDto>> GetById(int id)
    {
        User? user = await _context.Users.Where(u => u.Id == id && u.DeletedAt == null).FirstOrDefaultAsync();
        if (user == null) throw new KeyNotFoundException("Usuario não encontrado");
        var response = _mapper.Map<UserResponseDto>(user);
        return Ok(response);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Post(UserCreateDto dto)
    {
        var user = _mapper.Map<User>(dto);
        user.Password = _passwordHashService.HashPassword(dto.Password);
        _context.Users.Add(user);

        await _context.SaveChangesAsync();

        var response = _mapper.Map<UserResponseDto>(user);

        return CreatedAtAction(nameof(GetById), new { id = user.Id }, response);
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        User? user = await _context.Users.Where(u => u.Id == id && u.DeletedAt == null).FirstOrDefaultAsync();
        if (user == null) throw new KeyNotFoundException("Usuario não encontrado");

        user.DeletedAt = DateTime.UtcNow;
        _context.Update(user);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, UserUpdateDto dto)
    {
        User? user = await _context.Users.Where(u => u.Id == id && u.DeletedAt == null).FirstOrDefaultAsync();
        if (user == null) throw new KeyNotFoundException("Usuario não encontrado");

        _mapper.Map(dto, user);

        user.UpdatedAt = DateTime.UtcNow;
        user.Password = _passwordHashService.HashPassword(dto.Password);

        await _context.SaveChangesAsync();

        var response = _mapper.Map<UserResponseDto>(user);
        return Ok(response);
    }
}