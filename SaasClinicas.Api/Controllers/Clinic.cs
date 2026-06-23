using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaasClinicas.Api.Data;
using SaasClinicas.Api.Dtos.Clinics;
using SaasClinicas.Api.Models;
using SaasClinicas.Api.Repositories;
using SaasClinicas.Api.Validators.Clinics;

namespace SaasClinicas.Api.Controllers;

[ApiController]
[Route("api/clinics")]
public class ClinicController : ControllerBase
{

    private readonly IRepository<Clinic> _repository;
    private readonly IMapper _mapper;

    public ClinicController(IRepository<Clinic> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<ClinicResponseDto>>> Get()
    {
        var clinics = await _repository.GetAllAsync();
        var response = _mapper.Map<List<ClinicResponseDto>>(clinics);
        return Ok(response);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Post(ClinicCreateDto dto)
    {
        var clinic = _mapper.Map<Clinic>(dto);
        var createdClinic = await _repository.AddAsync(clinic);
        var response = _mapper.Map<ClinicResponseDto>(createdClinic);
        return CreatedAtAction(nameof(GetById), new { id = clinic.Id }, response);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<ClinicResponseDto>> GetById(int id)
    {
        var clinic = await _repository.GetByIdAsync(id);
        if (clinic == null)
            throw new KeyNotFoundException("Clinica não encontrada");
        var response = _mapper.Map<ClinicResponseDto>(clinic);

        return Ok(response);
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, ClinicUpdateDto dto)
    {
        Clinic? clinic = await _repository.GetByIdAsync(id);

        if (clinic == null)
            throw new KeyNotFoundException("Clinica não encontrada");
        
        dto.Id = id;
        _mapper.Map(dto, clinic);
        await _repository.UpdateAsync(clinic);
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        Clinic? clinic = await _repository.GetByIdAsync(id);

        if (clinic == null)
            throw new KeyNotFoundException("Clinica não encontrada");

        await _repository.DeleteAsync(clinic);
        return NoContent();
    }
}