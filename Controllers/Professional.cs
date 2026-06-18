using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaasClinicas.Api.Data;
using SaasClinicas.Api.Dtos.Professionals;
using SaasClinicas.Api.Models;

namespace SaasClinicas.Api.Controllers;

[ApiController]
[Route("api/professionals")]
public class ProfessionalsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ProfessionalsController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<ProfessionalResponseDto>>> Get()
    {
        List<Professional> professionals = await _context.Professionals.Where(p => p.DeletedAt == null).ToListAsync();
        var response = _mapper.Map<List<ProfessionalResponseDto>>(professionals);
        return Ok(response);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<ProfessionalResponseDto>> GetById(int id)
    {
        Professional? professional = await _context.Professionals.Where(p => p.Id == id && p.DeletedAt == null).FirstOrDefaultAsync();

        if (professional == null) return NotFound();

        var response = _mapper.Map<ProfessionalResponseDto>(professional);

        return Ok(response);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Post(ProfessionalCreateDto dto)
    {
        var professional = _mapper.Map<Professional>(dto);

        _context.Professionals.Add(professional);
        await _context.SaveChangesAsync();

        var response = _mapper.Map<ProfessionalResponseDto>(professional);
        return CreatedAtAction(nameof(GetById), new { id = professional.Id }, response);
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        Professional? professional = await _context.Professionals.Where(p => p.Id == id && p.DeletedAt == null).FirstOrDefaultAsync();

        if (professional == null) return NotFound();

        professional.DeletedAt = DateTime.UtcNow;
        _context.Update(professional);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, ProfessionalUpdateDto dto)
    {
        Professional? professional = await _context.Professionals.Where(p => p.Id == id && p.DeletedAt == null).FirstOrDefaultAsync();

        if (professional == null) return NotFound();

        _mapper.Map(dto, professional);
        professional.UpdatedAt = DateTime.UtcNow;
        _context.Update(professional);
        await _context.SaveChangesAsync();

        var response = _mapper.Map<ProfessionalResponseDto>(professional);

        return Ok(response);
    }
}