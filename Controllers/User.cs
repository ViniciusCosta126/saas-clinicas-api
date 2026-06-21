using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaasClinicas.Api.Data;
using SaasClinicas.Api.Dtos.Users;
using SaasClinicas.Api.Models;
using SaasClinicas.Api.Repositories;
using SaasClinicas.Api.Services;
using SaasClinicas.Api.Validators.Users;

namespace SaasClinicas.Api.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{

    private readonly IRepository<User> _repository;
    private readonly IMapper _mapper;
    private readonly IPasswordHashService _passwordHashService;
    public UsersController(IRepository<User> repository, IMapper mapper, IPasswordHashService passwordHashService)
    {
        _repository = repository;
        _mapper = mapper;
        _passwordHashService = passwordHashService;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<UserResponseDto>>> Get()
    {
        var users = await _repository.GetAllAsync();

        var response = _mapper.Map<List<UserResponseDto>>(users);

        return Ok(response);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<UserResponseDto>> GetById(int id)
    {
        User? user = await _repository.GetByIdAsync(id);
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

        var createdUser = await _repository.AddAsync(user);

        var response = _mapper.Map<UserResponseDto>(createdUser);

        return CreatedAtAction(nameof(GetById), new { id = createdUser.Id }, response);
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        User? user = await _repository.GetByIdAsync(id);
        if (user == null) throw new KeyNotFoundException("Usuario não encontrado");

        await _repository.DeleteAsync(user);

        return NoContent();
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, UserUpdateDto dto)
    {

        User? user = await _repository.GetByIdAsync(id);
        if (user == null) throw new KeyNotFoundException("Usuario não encontrado");

        dto.Id = id;
        _mapper.Map(dto, user);
        await _repository.UpdateAsync(user);
        var response = _mapper.Map<UserResponseDto>(user);

        return Ok(response);
    }
}