using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaasClinicas.APi.Data;
using SaasClinicas.APi.Dtos.Clinics;
using SaasClinicas.APi.Models;

namespace SaasClinicas.APi.Controllers;

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

    [HttpGet]
    public async Task<ActionResult<List<ClinicResponseDto>>> Get()
    {
        List<Clinic> clinics = await _context.Clinics.Where(c => c.DeletedAt == null).ToListAsync();
        var response = _mapper.Map<List<ClinicResponseDto>>(clinics);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post(ClinicCreateDto dto)
    {
        var clinic = _mapper.Map<Clinic>(dto);
        _context.Clinics.Add(clinic);
        await _context.SaveChangesAsync();
        var response = _mapper.Map<ClinicResponseDto>(clinic);
        return CreatedAtAction(nameof(GetById), new { id = clinic.Id }, response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ClinicResponseDto>> GetById(int id)
    {
        var clinic = await _context.Clinics
            .Where(c => c.Id == id)
            .Where(c => c.DeletedAt == null)
            .FirstOrDefaultAsync();

        if (clinic == null)
            return NotFound();

        var response = _mapper.Map<ClinicResponseDto>(clinic);

        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, ClinicUpdateDto dto)
    {
        Clinic? clinic = await _context.Clinics.FirstOrDefaultAsync(c => c.Id == id && c.DeletedAt == null);

        if (clinic == null) return NotFound();

        _mapper.Map(dto, clinic);

        clinic.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        Clinic? clinic = await _context.Clinics.FirstOrDefaultAsync(c => c.Id == id && c.DeletedAt == null);

        if (clinic == null) return NotFound();

        clinic.DeletedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return NoContent();
    }
}