using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaasClinicas.Api.Data;
using SaasClinicas.Api.Dtos.Patients;
using SaasClinicas.Api.Models;
using SaasClinicas.Api.Repositories;
using SaasClinicas.Api.Validators.Patients;

namespace SaasClinicas.Api.Controllers;

[ApiController]
[Route("api/patients")]
public class PatientsController : ControllerBase
{
    private readonly IRepository<Patient> _repository;
    private readonly IMapper _mapper;

    public PatientsController(IRepository<Patient> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<PatientResponseDto>>> Get()
    {
        var patients = await _repository.GetAllAsync();

        var response = _mapper.Map<List<PatientResponseDto>>(patients);

        return Ok(response);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<PatientResponseDto>> GetById(int id)
    {
        Patient? patient = await _repository.GetByIdAsync(id);

        if (patient == null)
            throw new KeyNotFoundException("Paciente não encontrado");

        var response = _mapper.Map<PatientResponseDto>(patient);

        return Ok(response);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult> Post(PatientCreateDto dto)
    {
        var patient = _mapper.Map<Patient>(dto);
        var createdPatient = await _repository.AddAsync(patient);
        var response = _mapper.Map<PatientResponseDto>(createdPatient);

        return CreatedAtAction(nameof(GetById), new { id = createdPatient.Id }, response);
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        Patient? patient = await _repository.GetByIdAsync(id);

        if (patient == null) throw new KeyNotFoundException("Paciente não encontrado");
        
        await _repository.DeleteAsync(patient);

        return NoContent();
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, PatientUpdateDto dto)
    {
        Patient? patient = await _repository.GetByIdAsync(id);

        if (patient == null) throw new KeyNotFoundException("Paciente não encontrado");

        dto.Id = id;
        _mapper.Map(dto,patient);
        
        await _repository.UpdateAsync(patient);

        var response = _mapper.Map<PatientResponseDto>(patient);

        return Ok(response);
    }
}