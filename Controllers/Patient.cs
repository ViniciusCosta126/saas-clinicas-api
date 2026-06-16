using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaasClinicas.Api.Data;
using SaasClinicas.Api.Dtos.Patients;
using SaasClinicas.Api.Models;

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

    [HttpGet]
    public async Task<ActionResult<List<PatientResponseDto>>> Get()
    {
        List<Patient> patients = await _context.Patients.Where(p => p.DeletedAt == null).ToListAsync();

        var response = _mapper.Map<List<PatientResponseDto>>(patients);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PatientResponseDto>> GetById(int id)
    {
        Patient? patient = await _context.Patients.Where(p => p.Id == id && p.DeletedAt == null).FirstOrDefaultAsync();

        if (patient == null) return NotFound();

        var response = _mapper.Map<PatientResponseDto>(patient);

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult> Post(PatientCreateDto dto)
    {
        var patient = _mapper.Map<Patient>(dto);

        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();

        var response = _mapper.Map<PatientResponseDto>(patient);
        return CreatedAtAction(nameof(GetById), new { id = patient.Id }, response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        Patient? patient = await _context.Patients.Where(p => p.Id == id && p.DeletedAt == null).FirstOrDefaultAsync();

        if (patient == null) return NotFound();

        patient.DeletedAt = DateTime.UtcNow;
        _context.Update(patient);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, PatientUpdateDto dto)
    {
        Patient? patient = await _context.Patients.Where(p => p.Id == id && p.DeletedAt == null).FirstOrDefaultAsync();

        if (patient == null) return NotFound();

        _mapper.Map(dto, patient);
        _context.Update(patient);
        await _context.SaveChangesAsync();

        var response = _mapper.Map<PatientResponseDto>(patient);

        return Ok(response);
    }
}