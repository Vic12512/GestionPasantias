using Microsoft.AspNetCore.Mvc;
using GestionPasantias.Application.Interfaces;
using GestionPasantias.Application.DTOs.Auth;
using GestionPasantias.Domain.Entities;


namespace GestionPasantias.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public AuthController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {   
        /////////////////
        /// VALIDATIONS
        /////////////////
         if (string.IsNullOrWhiteSpace(dto.Email))
            return BadRequest("Email is required");

        if (string.IsNullOrWhiteSpace(dto.PasswordHash))
            return BadRequest("Password is required");

        if (dto.RolId <= 0)
            return BadRequest("Invalid Role");

        if (await _userRepository.EmailExistAsync(dto.Email))
            return BadRequest("Email is already registered");

        /////////////////
        /// CREATION
        /////////////////
        var user = new User
        {
            Email = dto.Email,
            PasswordHash = dto.PasswordHash,
            RolId = dto.RolId
        };

        var createdUser = await _userRepository.AddAsync(user);


        /////////////////
        /// RESPONSE
        /////////////////
        return Ok(new
        {
            createdUser.Id,
            createdUser.Email,
            createdUser.RolId
        });
    }
}

