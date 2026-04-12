using Microsoft.AspNetCore.Mvc;
using GestionPasantias.Application.DTOs.Postulaciones;
using GestionPasantias.Application.Interfaces;
using GestionPasantias.Domain.Entities;
using GestionPasantias.Domain.Constants;

namespace GestionPasantias.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostulacionesController : ControllerBase
{
    private readonly IPostulacionRepository _postulacionRepository;
    private readonly IPasantiaRepository _pasantiaRepository;

    public PostulacionesController(
        IPostulacionRepository postulacionRepository, 
        IPasantiaRepository pasantiaRepository
    )
    {
        _postulacionRepository = postulacionRepository;
        _pasantiaRepository = pasantiaRepository;
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

        if (dto.EstudianteId <= 0)
            return BadRequest("Invalid Student.");

        if (dto.VacanteId <= 0)
            return BadRequest("Invalid Vacancy.");

        /////////////////
        /// CREATION
        /////////////////
        var postulacion = new Postulacion
        {
            EstudianteId = dto.EstudianteId,
            VacanteId = dto.VacanteId,
            EstadoPostulacionId = EstadoPostulacionConst.PendienteTutor,
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

    [HttpPut("{id}/aprobar-tutor")]
    public async Task<IActionResult> AprobarTutor (int id, AprobarPostulacionDto dto)
    {
        var postulacion = await _postulacionRepository.GetByIdAsync(id);

        if (postulacion == null)
            return NotFound();

        if (id <= 0)
            return BadRequest("Invalid application id.");

        if (postulacion. EstadoPostulacionId != EstadoPostulacionConst.PendienteTutor)
            return BadRequest("This application in not pending");
        
        postulacion.EstadoPostulacionId = dto.Aprobada 
            ? EstadoPostulacionConst.AprobadaTutor 
            : EstadoPostulacionConst.RechazadaSupervisor;

        await _postulacionRepository.UpdateAsync(postulacion);

        return Ok(new
        {
           Menssage = dto.Aprobada ? "Application approved by tutor." : "Application rejected by tutor",
           postulacion.Id,
           postulacion.EstadoPostulacionId
        });
    }

    [HttpPut("{id}/aprobar-supervisor")]
    public async Task<IActionResult> AprobarSupervisor(int id, AprobarPostulacionDto dto)
    {
        var postulacion = await _postulacionRepository.GetByIdAsync(id);

        if (postulacion == null)
            return NotFound();

        if (id <= 0)
            return BadRequest("Invalid application id.");

        if (postulacion.EstadoPostulacionId != EstadoPostulacionConst.AprobadaTutor)
            return BadRequest("This application was denied by the Tutor");
        
        if (!dto.Aprobada)
        {
            postulacion.EstadoPostulacionId = EstadoPostulacionConst.RechazadaSupervisor;
            await _postulacionRepository.UpdateAsync(postulacion);

            return Ok(new
            {
                Message = "Applicantion Rejected By Supervisor.",
                postulacion.Id,
                postulacion.EstadoPostulacion
            });
        }

        postulacion.EstadoPostulacionId = EstadoPostulacionConst.AprobadaSupervisor;
        await _postulacionRepository.UpdateAsync(postulacion);

        var pasantiaExist = await _pasantiaRepository.ExistByPostulacionIdAsync(postulacion.Id);
        if (pasantiaExist)
            return BadRequest("This application already has an intership created.");

        var pasantia = new Pasantia
        {
            PostulacionId = postulacion.Id,
            FechaInicio = postulacion.Vacante.FechaInicio,
            FechaFin = postulacion.Vacante.FechaFin
        };

        var createdPassantia = await _pasantiaRepository.AddAsync(pasantia);

        return Ok(new
        {
            Message = "Application approved by supervisor. Intership created.",
            postulacion.Id,
            postulacion.EstadoPostulacionId,
            PasantiaId = createdPassantia.Id,
            createdPassantia.FechaInicio,
            createdPassantia.FechaFin
        });
    }

}