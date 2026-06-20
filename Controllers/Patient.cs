using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaasClinicas.Api.Data;
using SaasClinicas.Api.Dtos.Patients;
using SaasClinicas.Api.Models;
using SaasClinicas.Api.Validators.Patients;

namespace SaasClinicas.Api.Controllers;

[ApiController]
[Route("api/patients")]
public class PatientsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public PatientsController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<PatientResponseDto>>> Get()
    {
        List<Patient> patients = await _context.Patients.Where(p => p.DeletedAt == null).ToListAsync();

        var response = _mapper.Map<List<PatientResponseDto>>(patients);

        return Ok(response);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<PatientResponseDto>> GetById(int id)
    {
        Patient? patient = await _context.Patients.Where(p => p.Id == id && p.DeletedAt == null).FirstOrDefaultAsync();

        if (patient == null)
            throw new KeyNotFoundException("Clinica não encontrada");

        var response = _mapper.Map<PatientResponseDto>(patient);

        return Ok(response);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult> Post(PatientCreateDto dto)
    {
        var patient = _mapper.Map<Patient>(dto);

        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();

        var response = _mapper.Map<PatientResponseDto>(patient);
        return CreatedAtAction(nameof(GetById), new { id = patient.Id }, response);
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        Patient? patient = await _context.Patients.Where(p => p.Id == id && p.DeletedAt == null).FirstOrDefaultAsync();

        if (patient == null) throw new KeyNotFoundException("Paciente não encontrado");

        patient.DeletedAt = DateTime.UtcNow;
        _context.Update(patient);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, PatientUpdateDto dto)
    {
        Patient? patient = await _context.Patients.Where(p => p.Id == id && p.DeletedAt == null).FirstOrDefaultAsync();

        if (patient == null) throw new KeyNotFoundException("Paciente não encontrado");

        var validator = new PatientUpdateValidator(_context, id);
        var result = await validator.ValidateAsync(dto);

        if (!result.IsValid) throw new ValidationException(result.Errors);

        _mapper.Map(dto, patient);
        _context.Update(patient);
        await _context.SaveChangesAsync();

        var response = _mapper.Map<PatientResponseDto>(patient);

        return Ok(response);
    }
}