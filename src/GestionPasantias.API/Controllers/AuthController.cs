using Microsoft.AspNetCore.Mvc;
using GestionPasantias.Application.Interfaces;
using GestionPasantias.Application.DTOs.Auth;
using GestionPasantias.Domain.Entities;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;


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

        if (string.IsNullOrWhiteSpace(dto.Password))
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
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
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

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var user = await _userRepository.GetByEmailAsync(dto.Email);

            if (user == null)
                return Unauthorized("User doesn't exist");

            var isValidPassword = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

            if(!isValidPassword)
                return Unauthorized("Invalid credentials");
        
        var token = GenerateJwtToken(user);

        return Ok(new { 
            token,
            user.Id,
            user.Email,
            user.RolId
         });
    }

    [Authorize]
    [HttpGet("source")]
    public IActionResult SecureEndPoint()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var email = User.FindFirst(ClaimTypes.Email)?.Value;
        var rol = User.FindFirst(ClaimTypes.Role)?.Value;   
        
        return Ok(new
        {
           Message = "Authentication working",
           UserId = userId,
           Email = email, 
           Rol = rol
        });
    }

    private string GenerateJwtToken(User user)
    {
        var config = HttpContext.RequestServices.GetRequiredService<IConfiguration>();

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(config["Jwt:Key"]!)
        );

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim("id", user.Id.ToString()),
            new Claim("email", user.Email),
            new Claim("role", user.RolId.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: config["Jwt:Issuer"],
            audience: config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(4),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

