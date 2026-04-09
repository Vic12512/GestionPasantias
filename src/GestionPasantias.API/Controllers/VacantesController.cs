using Microsoft.AspNetCore.Mvc;
using GestionPasantias.Application.DTOs.Vacantes;
using GestionPasantias.Application.Interfaces;
using GestionPasantias.Domain.Entities;

namespace GestionPasantias.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VacantesController : ControllerBase
{
    private readonly IVacanteRepository _vacanteRepository;

    public VacantesController(IVacanteRepository vacanteRepository)
    {
        _vacanteRepository = vacanteRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var vacantes = await _vacanteRepository.GetAllAsync();

        var result = vacantes.Select(v => new VacanteDto
        {
            Id = v.Id,
            EmpresaId = v.EmpresaId,
            SupervisorId = v.SupervisorId,
            Titulo = v.Titulo,
            Descripcion = v.Descripcion,
            Cantidad = v.Cantidad,
            FechaInicio = v.FechaInicio,
            FechaFin = v.FechaFin
        });

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetVacante(int id)
    {
        var vacante = await _vacanteRepository.GetByIdAsync(id);

         if(vacante == null)
            return NotFound();

        var result = new VacanteDto
        {
            Id = vacante.Id,
            EmpresaId = vacante.EmpresaId,
            SupervisorId = vacante.SupervisorId,
            Titulo = vacante.Titulo,
            Descripcion = vacante.Descripcion,
            Cantidad = vacante.Cantidad,
            FechaInicio = vacante.FechaInicio,
            FechaFin = vacante.FechaFin
        };

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateVacanteDto dto)
    {
        /////////////////
        /// VALIDATIONS
        /////////////////
        if(string.IsNullOrWhiteSpace(dto.Titulo))
            return BadRequest("Tittle is mandatory");
        
        if(dto.Cantidad <= 0)
            return BadRequest("The amount must be greater than 0");

        if(dto.FechaFin <= dto.FechaInicio)
            return BadRequest("The End date must be greater than the Start day");
        
        var empresaExist = await _vacanteRepository.EmpresaExistAsync(dto.EmpresaId);
        if(!empresaExist)
            return BadRequest("Selected company doesn't exist");

        var supervisorExist = await _vacanteRepository.SupervisorExistAsync(dto.SupervisorId);
        if(!supervisorExist)
            return BadRequest("Selected supervisor doesn't exist");

        /////////////////
        /// CREATION
        /////////////////
        var vacante = new Vacante
        {
            EmpresaId = dto.EmpresaId,
            SupervisorId = dto.SupervisorId,
            Titulo = dto.Titulo,
            Descripcion = dto.Descripcion,
            Cantidad = dto.Cantidad,
            FechaInicio = dto.FechaInicio,
            FechaFin = dto.FechaFin
        };

        var created = await _vacanteRepository.AddAsync(vacante);

        var result = new VacanteDto
        {
            Id = created.Id,
            EmpresaId = created.EmpresaId,
            SupervisorId = created.SupervisorId,
            Titulo = created.Titulo,
            Descripcion = created.Descripcion,
            Cantidad = created.Cantidad,
            FechaInicio = created.FechaInicio,
            FechaFin = created.FechaFin
        };

        return CreatedAtAction(nameof(GetVacante), new {id = created.Id, result});
    }
    
}