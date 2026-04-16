using Microsoft.AspNetCore.Mvc;
using GestionPasantias.Application.DTOs.Pasantias;
using GestionPasantias.Application.Interfaces;


namespace GestionPasantias.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PasantiasController : ControllerBase
{
    private readonly IPasantiaRepository _pasantiaRepository;

    public PasantiasController(IPasantiaRepository pasantiaRepository)
    {
        _pasantiaRepository = pasantiaRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var pasantias = await _pasantiaRepository.GetAllAsync();

        var result = pasantias.Select(p => new PasantiaDto
        {
            Id = p.Id,
            PostulacionId = p.PostulacionId,
            FechaInicio = p.FechaInicio,
            FechaFin = p.FechaFin
        });

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPasantia(int id)
    {
        var pasantia = await _pasantiaRepository.GetByIdAsync(id);

        if (pasantia == null)
            return NotFound();
        
        var result = new PasantiaDto
        {
            Id = pasantia.Id,
            PostulacionId = pasantia.PostulacionId,
            FechaInicio = pasantia.FechaInicio,
            FechaFin = pasantia.FechaFin
        };

        return Ok(result);
    }
}