using Microsoft.AspNetCore.Mvc;
using GestionPasantias.Application.DTOs.Evaluaciones;
using GestionPasantias.Application.Interfaces;
using GestionPasantias.Domain.Entities;
using System.Xml;

namespace GestionPasantias.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EvaluacionesController : ControllerBase
{
    private readonly IEvaluacionRepository _evaluacionesRepository;

    public EvaluacionesController(IEvaluacionRepository evaluacionRepository)
    {
        _evaluacionesRepository = evaluacionRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var evaluaciones = await _evaluacionesRepository.GetAllAsync();

        var result = evaluaciones.Select(e => new EvaluacionDto
        {
            Id = e.Id,
            PasantiaId = e.PasantiaId,
            SupervisorId = e.SupervisorId,
            Calificacion = e.Calificacion,
            Comentarios = e.Comentarios,
            Fecha = e.Fecha
        });

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEvaluacion(int id)
    {
        var evaluacion = await _evaluacionesRepository.GetByIdAsync(id);

        if (evaluacion == null)
            return NotFound();

        var result = new EvaluacionDto
        {
            Id = evaluacion.Id,
            PasantiaId = evaluacion.PasantiaId,
            SupervisorId = evaluacion.SupervisorId,
            Calificacion = evaluacion.Calificacion,
            Comentarios = evaluacion.Comentarios,
            Fecha = evaluacion.Fecha
        };

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateEvaluacionDto dto)
    {
        if (dto.PasantiaId <= 0)
            return BadRequest("Invalid intership.");

        if (dto.SupervisorId <= 0)
            return BadRequest("Invalid supervisor.");

        if (dto.Calificacion < 0 || dto.Calificacion > 100)
            return BadRequest("The score must be between 0 and 100.");

        var pasantiaExist = await _evaluacionesRepository.PasantiaExistAsync(dto.PasantiaId);
        if(!pasantiaExist)
            return BadRequest("Selected intership doesn't exist.");

        var supervisorExist = await _evaluacionesRepository.SupervisorExistAsync(dto.SupervisorId);
        if(!supervisorExist)
            return BadRequest("Selected supervisor doesn't exist.");
        
        var evaluacionExist = await _evaluacionesRepository.ExistByPasantiaIdAsync(dto.PasantiaId);
        if(evaluacionExist)
            return BadRequest("This intership already has an evaluation.");
        
        var evaluacion = new Evaluacion
        {
            PasantiaId = dto.PasantiaId,
            SupervisorId = dto.SupervisorId,
            Calificacion = dto.Calificacion,
            Comentarios = dto.Comentarios,
            Fecha = DateTime.UtcNow
        };

        var created = await _evaluacionesRepository.AddAsync(evaluacion);

        var result = new EvaluacionDto
        {
            Id = created.Id,
            PasantiaId = created.PasantiaId,
            SupervisorId = created.SupervisorId,
            Calificacion = created.Calificacion,
            Comentarios = created.Comentarios,
            Fecha = created.Fecha
        };

        return CreatedAtAction(nameof(GetEvaluacion), new {id = created.Id}, result);
    }
}