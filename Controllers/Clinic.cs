using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaasClinicas.Api.Data;
using SaasClinicas.Api.Dtos.Clinics;
using SaasClinicas.Api.Models;
using SaasClinicas.Api.Validators.Clinics;

namespace SaasClinicas.Api.Controllers;

[ApiController]
[Route("api/clinics")]
public class ClinicController : ControllerBase
{

    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ClinicController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<ClinicResponseDto>>> Get()
    {
        List<Clinic> clinics = await _context.Clinics.Where(c => c.DeletedAt == null).ToListAsync();
        var response = _mapper.Map<List<ClinicResponseDto>>(clinics);
        return Ok(response);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Post(ClinicCreateDto dto)
    {
        var clinic = _mapper.Map<Clinic>(dto);
        _context.Clinics.Add(clinic);
        await _context.SaveChangesAsync();
        var response = _mapper.Map<ClinicResponseDto>(clinic);
        return CreatedAtAction(nameof(GetById), new { id = clinic.Id }, response);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<ClinicResponseDto>> GetById(int id)
    {
        var clinic = await _context.Clinics
            .Where(c => c.Id == id)
            .Where(c => c.DeletedAt == null)
            .FirstOrDefaultAsync();

        if (clinic == null)
            throw new KeyNotFoundException("Clinica não encontrada");
        var response = _mapper.Map<ClinicResponseDto>(clinic);

        return Ok(response);
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, ClinicUpdateDto dto)
    {
        Clinic? clinic = await _context.Clinics.FirstOrDefaultAsync(c => c.Id == id && c.DeletedAt == null);

        if (clinic == null)
            throw new KeyNotFoundException("Clinica não encontrada");

        var validator = new ClinicUpdateValidator(_context, id);
        var result = await validator.ValidateAsync(dto);
        if (!result.IsValid) throw new ValidationException(result.Errors);

        _mapper.Map(dto, clinic);

        clinic.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        Clinic? clinic = await _context.Clinics.FirstOrDefaultAsync(c => c.Id == id && c.DeletedAt == null);

        if (clinic == null)
            throw new KeyNotFoundException("Clinica não encontrada");

        clinic.DeletedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return NoContent();
    }
}