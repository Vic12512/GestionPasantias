using Microsoft.AspNetCore.Mvc;
using GestionPasantias.Application.Interfaces;
using GestionPasantias.Domain.Entities;
using System.Xml;

namespace GestionPasantias.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    
    public UsersController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userRepository.GetAllAsync();
        return Ok(users);
    }

    /*
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if(user == null)
            return NotFound();

        return Ok(user);
    }
    */
} 