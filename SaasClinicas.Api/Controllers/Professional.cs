using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaasClinicas.Api.Data;
using SaasClinicas.Api.Dtos.Professionals;
using SaasClinicas.Api.Models;
using SaasClinicas.Api.Repositories;
using SaasClinicas.Api.Validators.Professionals;

namespace SaasClinicas.Api.Controllers;

[ApiController]
[Route("api/professionals")]
public class ProfessionalsController : ControllerBase
{
    private readonly IRepository<Professional> _repository;
    private readonly IMapper _mapper;

    public ProfessionalsController(IRepository<Professional> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<ProfessionalResponseDto>>> Get()
    {
        var professionals = await _repository.GetAllAsync();
        var response = _mapper.Map<List<ProfessionalResponseDto>>(professionals);
        return Ok(response);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<ProfessionalResponseDto>> GetById(int id)
    {
        Professional? professional = await _repository.GetByIdAsync(id);

        if (professional == null) throw new KeyNotFoundException("Profisional não encontrado");

        var response = _mapper.Map<ProfessionalResponseDto>(professional);

        return Ok(response);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Post(ProfessionalCreateDto dto)
    {
        var professional = _mapper.Map<Professional>(dto);

        var createdProfissional = await _repository.AddAsync(professional);

        var response = _mapper.Map<ProfessionalResponseDto>(createdProfissional);
        return CreatedAtAction(nameof(GetById), new { id = createdProfissional.Id }, response);
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        Professional? professional = await _repository.GetByIdAsync(id);

        if (professional == null) throw new KeyNotFoundException("Profisional não encontrado");

        await _repository.DeleteAsync(professional);

        return NoContent();
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, ProfessionalUpdateDto dto)
    {
        Professional? professional = await _repository.GetByIdAsync(id);

        if (professional == null) throw new KeyNotFoundException("Profisional não encontrado");


        dto.Id = id;
        _mapper.Map(dto, professional);
        await _repository.UpdateAsync(professional);

        var response = _mapper.Map<ProfessionalResponseDto>(professional);

        return Ok(response);
    }
}