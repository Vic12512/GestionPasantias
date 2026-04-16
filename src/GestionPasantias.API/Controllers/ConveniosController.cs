using Microsoft.AspNetCore.Mvc;
using GestionPasantias.Application.Interfaces;
using GestionPasantias.Domain.Entities;
using GestionPasantias.Application.DTOs.Convenios;
using Microsoft.Identity.Client;

namespace GestionPasantias.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConveniosController : ControllerBase
{
    private readonly IConvenioRepository _convenioRepository;
    
    public ConveniosController(IConvenioRepository convenioRepository)
    {
        _convenioRepository = convenioRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var convenios = await _convenioRepository.GetAllAsync();

        var result = convenios.Select(c => new ConvenioDto
        {
           Id = c.Id,
           PasantiaId = c.PasantiaId,
           Numero = c.Numero,
           FechaGeneracion = c.FechaGeneracion,
           FechaInicio = c.FechaInicio,
           FechaFin = c.FechaFin,
           NombreEstudiante = c.NombreEstudiante,
           NombreEmpresa = c.NombreEmpresa,
           NombreUniversidad = c.NombreUniversidad
        });

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetConvenio(int id)
    {
        var convenio = await _convenioRepository.GetByIdAsync(id);

        if (convenio == null) 
            return NotFound();

        var result = new ConvenioDto
        {
            Id = convenio.Id,
            PasantiaId = convenio.PasantiaId,
            Numero = convenio.Numero,
            FechaGeneracion = convenio.FechaGeneracion,
            FechaInicio = convenio.FechaInicio,
            FechaFin = convenio.FechaFin,
            NombreEstudiante = convenio.NombreEstudiante,
            NombreEmpresa = convenio.NombreEmpresa,
            NombreUniversidad = convenio.NombreUniversidad
        };

        return Ok(result);
    }
}