using Microsoft.AspNetCore.Mvc;
using GestionPasantias.Application.DTOs.Postulaciones;
using GestionPasantias.Application.Interfaces;
using GestionPasantias.Domain.Entities;

namespace GestionPasantias.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostulacionesController : ControllerBase
{
    private readonly IPostulacionRepository _postulacionRepository;

    public PostulacionesController(IPostulacionRepository postulacionRepository)
    {
        _postulacionRepository = postulacionRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var postulaciones = await _postulacionRepository.GetAllAsync();

        var result = postulaciones.Select(p => new PostulacionDto
        {
            Id = p.Id,
            EstudianteId = p.EstudianteId,
            VacanteId = p.VacanteId,
            EstadoPostulacionId = p.EstadoPostulacionId,
            FechaPostulacion = p.FechaPostulacion
        });

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPostulacion(int id)
    {
        var postulacion = await _postulacionRepository.GetByIdAsync(id);

         if(postulacion == null)
            return NotFound();

        var result = new PostulacionDto
        {
            Id = postulacion.Id,
            EstudianteId = postulacion.EstudianteId,
            VacanteId = postulacion.VacanteId,
            EstadoPostulacionId = postulacion.EstadoPostulacionId,
            FechaPostulacion = postulacion.FechaPostulacion
        };

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreatePostulacionDto dto)
    {
        /////////////////
        /// VALIDATIONS
        /////////////////
        var estudianteExist = await _postulacionRepository.EstudianteExistAsync(dto.EstudianteId);
        if(!estudianteExist)
            return BadRequest("Selected student doesn't exist");

        var vacanteExist = await _postulacionRepository.VacanteExistAsync(dto.VacanteId);
        if(!vacanteExist)
            return BadRequest("Selected vacacy doesn't exist");

        var alreadyExist = await _postulacionRepository.PostulacionExistAsync(dto.EstudianteId, dto.VacanteId);
        if(alreadyExist)
            return BadRequest("Already applied to this vacancy.");

        /////////////////
        /// CREATION
        /////////////////
        var postulacion = new Postulacion
        {
            EstudianteId = dto.EstudianteId,
            VacanteId = dto.VacanteId,
            EstadoPostulacionId = 1,
            FechaPostulacion = DateTime.UtcNow
        };

        var created = await _postulacionRepository.AddAsync(postulacion);

        var result = new PostulacionDto
        {
            Id = postulacion.Id,
            EstudianteId = postulacion.EstudianteId,
            VacanteId = postulacion.VacanteId,
            EstadoPostulacionId = postulacion.EstadoPostulacionId,
            FechaPostulacion = postulacion.FechaPostulacion
        };
        
        return CreatedAtAction(nameof(GetPostulacion), new {id = created.Id}, result);

    }

}